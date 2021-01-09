# Getting Started
Here you can read how to get started with the code.

# Setting up the database
We work with MS SQL here. To use MS SQL, we need a Microsoft SQL Server. The free version of this is called Microsoft SQL Express.

## MacOS
However, Since I am working on MacOS privately and there is not Microsoft SQL Server for MacOS, I need to use a workaround: Docker. So I've installed Docker and then pulled this Docker image: https://hub.docker.com/_/microsoft-mssql-server

To achieve this I had to execute the following:
```
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Test1234' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```

> We are using the very simple password **Test1234**. On the test server and productive server we should not use a password anyway, but rather use an authentication by domain name or just allow access within the same network.

We can now connect to the database for example by using the Azure Data Studio and with the following connection data:

| Name     | Value     |
|:---------|:----------|
| Server   | localhost |
| Username | sa        |
| Password | Test1234  |

## Creating the database with migrations
We are using Entity Farmework Migrations here to keep the database up to date. Here's what's important for the initial setup (following this totorial: https://docs.microsoft.com/en-gb/ef/core/cli/dotnet:
- Make sure you have the Entity Framework tools installed:
  - ```dotnet tool install --global dotnet-ef```
- Setup your database using the migration scripts:
  - ```dotnet ef database update -- --environment Development```

Check out the page /database/migrations.md to see more commands regarding the migrations.