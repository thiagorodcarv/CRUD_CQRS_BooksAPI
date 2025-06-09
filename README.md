# 📚 CleanArch Livros API

Este projeto é uma API REST para gerenciamento de **livros**, **autores** e **gêneros**, construída em .NET 8 com os padrões **Clean Architecture**, **CQRS**, **MediatR**, **TDD**, **FluentValidation**, **Entity Framework** e banco de dados **SQL Server**.

---

## 🚀 Funcionalidades

- CRUD de Autores, Gêneros e Livros
- Listagem de livros com:
  - Paginação
  - Ordenação por título, nome do autor ou gênero
  - Filtro por termo
- Validações assíncronas com FluentValidation
- Integração com Swagger
- Separação de responsabilidades por camadas (Domain, Application, Infrastructure, API)

---

## 🧱 Arquitetura

```
CleanArch.API             → Camada de apresentação (controllers, Swagger)
CleanArch.Application     → Use Cases, Validations, ViewModels, Queries, Handlers
CleanArch.Domain          → Entidades e Interfaces
CleanArch.Infrastructure  → Implementações (repositórios, EF, contexto)
CleanArch.CrossCutting    → Injeção de dependência e comportamentos
```

---

## ⚙️ Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)
- [EF Tools CLI](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)  
  Instale com:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## 📝 Configuração

1. **Clone o projeto:**

   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   ```

2. **Configure a connection string:**

   Edite o arquivo:

   ```
   CleanArch.API/appsettings.Development.json
   ```

   E substitua pela sua connection string:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=SeuDataBase;User Id=sa;Password=SuaSenha123;"
   }
   ```
   ou algo assim
   ```json
   "ConnectionStrings": {
      "DefaultConnection": "Data Source=SeuServidor;Initial Catalog=SeuDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"
   }
   ```
---

## 🗃️ Gerar banco de dados

1. Navegue até a raiz da solução:

   ```bash
   cd src
   ```

2. Execute o comando de migration (o projeto Infrastructure contém o contexto):

   ```bash
   dotnet ef migrations add InitialCreate -p CleanArch.Infrastructure -s CleanArch.API -o Migrations
   ```

3. Aplique a migration no banco:

   ```bash
   dotnet ef database update -p CleanArch.Infrastructure -s CleanArch.API
   ```

---

## ▶️ Executar a aplicação

```bash
dotnet run --project CleanArch.API
```

Ou pelo Visual Studio / Rider, com o projeto `CleanArch.API` como startup.

---

## 📘 Swagger

Acesse a documentação da API no navegador:

```
https://localhost:5001/swagger
```

---

## 🧪 Testes

Para rodar os testes unitários:

```bash
dotnet test
```

Se quiser cobertura de testes:

```bash
./scripts/run-tests-with-coverage.bat
```

---

## 🛠️ Tecnologias usadas

- .NET 8
- Entity Framework Core
- SQL Server
- MediatR (CQRS)
- FluentValidation
- Swagger / Swashbuckle
- AutoMapper (opcional)
- xUnit + Moq

---

