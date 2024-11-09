## SistemaPedidosAPI

**SistemaPedidosAPI** é uma API RESTful construída em **ASP.NET Core** para gerenciar clientes, produtos e pedidos, utilizando **PostgreSQL** como banco de dados.

### Índice
- [Instalação](#instalação)
- [Configuração](#configuração)
- [Migrations e Inicialização do Banco de Dados](#migrations-e-inicialização-do-banco-de-dados)
- [Executando o Projeto](#executando-o-projeto)
- [Documentação dos Endpoints](#documentação-dos-endpoints)
- [Exemplos de Uso](#exemplos-de-uso)

---

### Instalação

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/seu-usuario/SistemaPedidosAPI.git
   cd SistemaPedidosAPI
   ```

2. **Instale as dependências** do projeto:
   ```bash
   dotnet restore
   ```

3. **Instale o PostgreSQL** (caso não tenha):
   - Para Debian/Ubuntu:
     ```bash
     sudo apt update
     sudo apt install postgresql postgresql-contrib
     ```
   - Para Windows, baixe o instalador do [site oficial do PostgreSQL](https://www.postgresql.org/download/).

4. **Crie o banco de dados**:
   - Acesse o PostgreSQL:
     ```bash
     sudo -u postgres psql
     ```
   - Crie o banco de dados `sistema_pedidos` e um usuário:
     ```sql
     CREATE DATABASE sistema_pedidos;
     CREATE USER seu_usuario WITH PASSWORD 'sua_senha';
     ALTER DATABASE sistema_pedidos OWNER TO seu_usuario;
     ```

---

### Configuração

1. **Configuração do banco de dados**:
   - No arquivo `appsettings.json`, atualize a string de conexão para apontar ao PostgreSQL:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=sistema_pedidos;Username=seu_usuario;Password=sua_senha;"
     }
     ```

2. **Configuração do DbContext**:
   - No arquivo `Program.cs`, a linha que configura o `ApplicationDbContext` deve usar `UseNpgsql` para o PostgreSQL:
     ```csharp
     builder.Services.AddDbContext<ApplicationDbContext>(options =>
         options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
     ```

---

### Migrations e Inicialização do Banco de Dados

1. **Gerar migrações**:
   - Caso o projeto ainda não possua migrações, execute:
     ```bash
     dotnet ef migrations add InitialCreate
     ```

2. **Aplicar migrações** ao banco de dados:
   ```bash
   dotnet ef database update
   ```

Esses passos criarão as tabelas e os relacionamentos no banco de dados PostgreSQL.

---

### Executando o Projeto

1. **Inicie o servidor**:
   ```bash
   dotnet run
   ```

2. **Acessar a API**:
   - O servidor estará disponível em `https://localhost:5001`.

3. **Swagger**:
   - A documentação interativa do Swagger estará disponível em: `https://localhost:5001/swagger`.
   - Aqui você pode visualizar, testar e documentar todos os endpoints da API.

---

### Documentação dos Endpoints

Abaixo estão os principais endpoints da API, organizados por entidade:

#### Endpoints de Cliente
- **GET** `/v1/clientes` - Retorna todos os clientes.
- **GET** `/v1/clientes/{id}` - Retorna um cliente específico.
- **POST** `/v1/clientes` - Cria um novo cliente.
- **PUT** `/v1/clientes/{id}` - Atualiza um cliente existente.
- **DELETE** `/v1/clientes/{id}` - Exclui um cliente.

#### Endpoints de Produto
- **GET** `/v1/produtos` - Retorna todos os produtos.
- **GET** `/v1/produtos/{id}` - Retorna um produto específico.
- **POST** `/v1/produtos` - Cria um novo produto.
- **PUT** `/v1/produtos/{id}` - Atualiza um produto existente.
- **DELETE** `/v1/produtos/{id}` - Exclui um produto.

#### Endpoints de Pedido
- **GET** `/v1/pedidos` - Retorna todos os pedidos.
- **GET** `/v1/pedidos/{id}` - Retorna um pedido específico.
- **POST** `/v1/pedidos` - Cria um novo pedido com produtos e quantidades.
- **PUT** `/v1/pedidos/{id}` - Atualiza o status ou os produtos de um pedido.
- **DELETE** `/v1/pedidos/{id}` - Exclui um pedido.

---

### Exemplos de Uso

#### Exemplo 1: Criar um Cliente
- **Requisição**: `POST /v1/clientes`
- **Corpo**:
  ```json
  {
    "nome": "João Silva",
    "email": "joao.silva@example.com",
    "numero_contato": "11999999999",
    "data_nascimento": "1985-05-10"
  }
  ```

#### Exemplo 2: Listar Produtos
- **Requisição**: `GET /v1/produtos`

#### Exemplo 3: Criar um Pedido
- **Requisição**: `POST /v1/pedidos`
- **Corpo**:
  ```json
  {
    "clienteId": 1,
    "status": "Em Andamento",
    "pedidoProdutos": [
      { "produtoId": 1, "quantidade": 2 },
      { "produtoId": 3, "quantidade": 1 }
    ]
  }
  ```

#### Exemplo 4: Atualizar o Status de um Pedido
- **Requisição**: `PUT /v1/pedidos/1`
- **Corpo**:
  ```json
  {
    "status": "Concluído",
    "pedidoProdutos": [
      { "produtoId": 1, "quantidade": 2 }
    ]
  }
  ```

---

### Observações
- Certifique-se de que o **PostgreSQL** está em execução.
- Caso altere a string de conexão, lembre-se de atualizar o `appsettings.json` e regenerar as migrações.
