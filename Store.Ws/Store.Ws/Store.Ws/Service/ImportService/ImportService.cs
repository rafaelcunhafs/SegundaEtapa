using Store.Data.Models;
using StoreWS.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StoreWS.Service
{
    public class ImportService : IImportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }               

        public void ImportFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            List<string[]> orderList = new List<string[]>();

            // Lê todas as linhas do arquivo
            if (File.Exists(filePath))
            {
                foreach (string line in lines.Skip(1))
                {
                    if (line.Length > 0)
                    {
                        // Separa as linhas por ';'
                        string[] orderLines = line.Split(';');
                        orderList.Add(orderLines);
                    }
                }

                // Variavel que recebe todos os pedidos por cliente
                Dictionary<string, List<string[]>> clientOrder = new Dictionary<string, List<string[]>>();
                
                // Separa cada pedido por cliente e caso tenha produto iguais, soma a quantidade
                foreach (string[] items in orderList)
                {
                    // Caso o cliente exista na lista
                    if(clientOrder.ContainsKey(items[0]))
                    {
                        List<string[]> order = clientOrder[items[0]];
                        bool isEqual = false;

                        // Testa se o produto já existe nos pedidos do cliente
                        foreach(string[] item in order)
                        {
                            if (item[0] == items[1])
                            {
                                item[1] = (int.Parse(item[1]) + int.Parse(items[2])).ToString();
                                isEqual = true;
                            }
                        }

                        // Se não, apenas adiciona o produto a lista
                        if(!isEqual)
                        {
                            string[] aux = new string[2];
                            aux[0] = items[1];
                            aux[1] = items[2];

                            order.Add(aux);

                            clientOrder[items[0]] = order;
                        }
                    }
                    // Caso o cliente não exista, adiciona o cliente e o pedido
                    else
                    {
                        List<string[]> order =  new List<string[]>();

                        string[] aux = new string[2];
                        aux[0] = items[1];
                        aux[1] = items[2];

                        order.Add(aux);

                        clientOrder.Add(items[0], order);
                    }
                }

                // Criando a lista de ordems e orderitems
                List<Order> orders = new List<Order>();
                List<OrderItem> orderItems = new List<OrderItem>();
                int number = 1;

                foreach (KeyValuePair<string, List<string[]>> order in clientOrder)
                {
                    // Para cada clientOrder, se cria uma order
                    Order newOrder = new Order();
                    newOrder.OrderId = Guid.NewGuid();

                    // Para cada dentro da order se cria uma orderItem
                    OrderItem newOrderItem = new OrderItem();
                    double value = 0;
                    foreach (var products in order.Value)
                    {
                        newOrderItem.OrderItemId = Guid.NewGuid();
                        newOrderItem.OrderId = newOrder.OrderId;
                        newOrderItem.ProductId = new Guid(products[0]);
                        newOrderItem.Quantity = int.Parse(products[1]);
                        orderItems.Add(newOrderItem);

                        value += int.Parse(products[1]);
                    }

                    newOrder.Number = number;
                    newOrder.ClientId = new Guid(order.Key);
                    newOrder.Value = value;
                    newOrder.CreatedOn = DateTime.Now;
                    number++;

                    orders.Add(newOrder);
                }

                // Salva tanto as orders como os orderItems
                _unitOfWork.OrderRepository.AddAll(orders);
                _unitOfWork.OrderItemRepository.AddAll(orderItems);
            }

        }
    }
}