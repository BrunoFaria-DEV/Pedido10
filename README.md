# Pedido10

## Descrição

Este é um sistema de gerenciamento de pedidos utilizando .NET Core, PostgreSQL e Docker. Abaixo estão as instruções para configuração do banco de dados e da aplicação.

## Requisitos

* .NET Core SDK 8.0
* Docker
* PostgreSQL

## Passos para Configuração

### 1. Criação do Banco de Dados

Para configurar o banco de dados manualmente, execute os seguintes passos:

#### 1.1. Acesse o PostgreSQL

Conecte-se ao seu servidor PostgreSQL usando um terminal ou ferramenta como `psql` ou `pgAdmin`. Caso utilize o terminal, execute o seguinte comando:

```bash
psql -U postgres
```

#### 1.2. Criação do Banco de Dados

Crie o banco de dados `Pedido10` com o seguinte comando SQL:

```sql
CREATE DATABASE Pedido10;
```

#### 1.3. Criação do Usuário

Crie um usuário `Pedido10` com a senha `Pedido10` e conceda permissões para o banco de dados:

```sql
CREATE USER Pedido10 WITH PASSWORD 'Pedido10';
GRANT ALL PRIVILEGES ON DATABASE Pedido10 TO Pedido10;
```

#### 1.4. Concedendo Permissões ao Banco de Dados

Conceda as permissões adequadas para o usuário recém-criado:

```sql
\c Pedido10
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO Pedido10;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO Pedido10;
```

#### 1.5. Criação das Tabelas

Agora, execute o script SQL para criar as tabelas necessárias no banco de dados `Pedido10`. Você pode usar o script que você forneceu, começando com a criação das tabelas como `Usuario`, `Forma_PGTO`, etc., incluindo triggers e funções:

```sql
-- Script para criação das tabelas conforme fornecido
-- Adicione o script completo aqui.
```

### 2. Configuração da Aplicação

#### 2.1. Restaurar Dependências

Para garantir que todas as dependências do projeto estejam corretamente instaladas, execute o comando:

```bash
dotnet restore
```

#### 2.2. Compilar o Projeto

Após restaurar as dependências, compile o projeto com:

```bash
dotnet build
```

#### 2.3. Criar a Imagem Docker

Depois de compilar a aplicação, crie a imagem Docker para o seu projeto:

```bash
sudo docker build -t pedido10-api .
```

#### 2.4. Rodar o Contêiner Docker

Agora, execute o contêiner Docker para rodar sua aplicação. O comando abaixo faz o mapeamento da porta 5000 do contêiner para a porta 8080 do contêiner:

```bash
sudo docker run -d -p 5000:8080 --name pedido10-container pedido10-api
```

### 3. Acessando a Aplicação

Após iniciar o contêiner, você pode acessar a aplicação na URL:

```
http://localhost:5000
```

### 4. Endpoints

#### 4.1. Endpoints Disponíveis

Abaixo, será possível listar os endpoints da sua aplicação. Quando você tiver a lista completa de endpoints, adicione-os aqui.

* **\[Endpoint 1]** Descrição do endpoint.
* **\[Endpoint 2]** Descrição do endpoint.

---

### 5. Testando a Aplicação

Você pode testar a aplicação utilizando ferramentas como [Postman](https://www.postman.com/) ou [Insomnia](https://insomnia.rest/), ou mesmo através do Swagger (caso tenha configurado).

#### 5.1. Acessando o Swagger

Caso tenha configurado o Swagger em sua aplicação, acesse a interface de documentação da API em:

```
http://localhost:5000/swagger
```

---

### 6. Conclusão

Com isso, o banco de dados está configurado e a aplicação está pronta para uso. Certifique-se de substituir os endpoints no local indicado no README assim que tiver a lista final de rotas da sua aplicação.

---

