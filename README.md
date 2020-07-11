# SegundaEtapa

## Descrição dos projetos
### Store.Web:
Projeto MVC responsável pela interface com o usuário. Deve conter a menor quantidade possível de regra de negócio.
### Store.WS:
Projeto WebService no qual deve ser implementada a regra de negócio que será consumida pelo projeto Store.Web.
### Store.Data:
Projeto responsável pela definição das entidades

### Faça no projeto Store.Data
Criar a classe OrderItem  e configurá-la como se a mesma fosse ser utilizada com o Entity Framework. Esta classe refere-se a tabela OrderItem do banco de dados fictício.
### Faça no projeto Store.Web
Criar tela que possibilite fazer o upload do arquivo Items.txt
Enviar os dados do arquivo para o projeto Store.WS.
Obs.: Não será avaliado o design da tela. Faça o mais simples possível.
###Faça no projeto Store.WS
- Criar método para receber o conteúdo do arquivo que será enviado do projeto Store.Web.
- Processar os dados do arquivo afim de se obter uma lista de pedidos (Order) e seus respectivos itens (OrderItem)
- Regras:
  - Deve ser gerado apenas um pedido por cliente.
  - No pedido de cada cliente, não devem existir produtos duplicados. (Caso haja mais de um produto por cliente no arquivo txt, a quantidade deles deve ser somada gerando apenas um item de pedido  para aquele produto)
Obs. Não é necessário persistir os dados em banco de dados. 
Caso seja necessário, os dados de clientes podem ser obtidos chamando o método Get que está implementado em Service.Client
Caso seja necessário, os dados de produtos podem ser obtidos chamando o método Get que está implementado em Service.Product
