# Big E-Commerce

BigECommerce é uma aplicação de e-commerce desenvolvida em .NET 8 com arquitetura modular, usando Domain Driven Design (DDD), MediatR, Entity Framework Core e PostgreSQL. A aplicação está preparada para execução via Docker.

## Tecnologias utilizadas
- .NET 8 (C#)
- ASP.NET Core Web API
- Entity Framework Core (MemoryDatabase)
- MediatR (CQRS e Commands/Queries)
- Docker (para containerização)
- xUnit (testes de unidade do domínio)

## Arquitetura da Solution
<img width="739" height="403" alt="image" src="https://github.com/user-attachments/assets/dee817c0-3133-494a-a8c6-bac949a22cbe" />

- A API contém os controllers e endpoints REST.
- Application possui comandos, queries e handlers (CQRS).
- Domain contém entidades, enums e regras de negócio.
- Infrastructure implementa o DbContext e o acesso ao banco.
- Auth mantém usuários e roles para autenticação.

## Usuários para testes

Para testes na api é necessário estar autenticado com um bearer token válido, existem dois usuários pré-configurados:

| Email                                                     | Senha     | Role   |
| --------------------------------------------------------- | --------- | ------ |
| [admin@bigecommerce.com](mailto:admin@bigecommerce.com)   | admin123  | Admin  |
| [client@bigecommerce.com](mailto:client@bigecommerce.com) | client123 | Client |

Obtenha o token para autenticação no swagger fazendo um POST em http://localhost:8080/auth/login:

<img width="1279" height="578" alt="image" src="https://github.com/user-attachments/assets/8d5a8293-20cc-4e47-996a-744ebaf81a48" />

## Executando a aplicação com Docker
#### 1. Clonar o repositório 
<img width="580" height="92" alt="image" src="https://github.com/user-attachments/assets/826e620d-3fa9-4a9e-b78e-07b254b1375a" />

#### 2. Build da imagem Docker
<img width="387" height="78" alt="image" src="https://github.com/user-attachments/assets/7fa63ce9-2578-4e97-a519-910c1afc3a87" />

#### 3. Rodar o container
<img width="720" height="55" alt="image" src="https://github.com/user-attachments/assets/a5bedf4e-87c9-4ef0-bb5e-1579be2602bf" />

#### 4. Acessar a API e Swagger

Abra o navegador em:

<img width="422" height="68" alt="image" src="https://github.com/user-attachments/assets/08b82df3-392f-432d-8e5b-17c61441f7ea" />

Você verá a documentação interativa da API:

<img width="1524" height="976" alt="image" src="https://github.com/user-attachments/assets/2635370c-3cdb-4779-bd43-045278018872" />

## Testes de domínio

O projeto possui testes unitários do Domain usando xUnit:

- OrderTests → valida criação de pedidos, adição de itens e pagamento.
- ProductTests → valida criação e atualização de produtos.
- PaymentTests → valida criação e confirmação de pagamentos.
- UserTests → valida criação de usuários.

#### Executar os testes:

<img width="444" height="74" alt="image" src="https://github.com/user-attachments/assets/d0d7dc4f-5cd0-4c0d-8fc2-8209e30f646c" />
