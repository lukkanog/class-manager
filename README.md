# Class Manager
Aplicação web para gerenciamento de alunos e turmas, sendo constituído de uma API Web e uma aplicação web feita em Blazor

## Premissas
- Ter o **.NET 8.0** instalado (versão >= 8.0.6)
- Ter uma instância do **SQL Server** em execução

## Executando o _Back-End_ em ambiente de desenvolvimento
1. Altere a _Connection String_ do banco de dados no arquivo `/src/Fiap.TesteTecnico.ClassManager.Api/appsettings.Development.json`
   
    ```json
    "ConnectionStrings": {
      "ClassManager": "Persist Security Info=False;Initial Catalog=class_manager;Server=localhost;User ID=sa;Password=SuperStrongPassword!;Trust Server Certificate=True"
    },
    ```
2. Dentro da raíz do repositório, execute o seguinte comando:

   ```bash
   dotnet run --project src/Fiap.TesteTecnico.ClassManager.Api/Fiap.TesteTecnico.ClassManager.Api.csproj
   ```
<br>

Após isso, deve ser possível ver a documentação _OpenApi_ pelo endereço `http://localhost:5239/swagger`

<br>

## Executando o Front-End em ambiente de desenvolvimento
1. Caso necessário, altere a rota da API no arquivo `/src/Fiap.TesteTecnico.ClassManager.WebApp/wwwroot/appsettings.Development.json`:
   
    ```json
    {
      "ApiRoute": "http://localhost:5239"
    }

    ```
2. Dentro da raíz do repositório, execute o seguinte comando:
   ```bash
   dotnet run --project src/Fiap.TesteTecnico.ClassManager.WebApp/Fiap.TesteTecnico.ClassManager.WebApp.csproj
   ```

<br>
Após isso, deve ser possível acessar a aplicação web pelo endereço `http://localhost:5278/`
<br>

## Banco de dados
O banco de dados é gerado automaticamente ao iniciar a API. Os scripts utilizados estão em `src/Fiap.TesteTecnico.ClassManager.Infra/Migrations`

