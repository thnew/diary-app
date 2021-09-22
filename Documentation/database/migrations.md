# How to use migrations

## Setting up the entity framework tools

## Updating the database
```
export ASPNETCORE_ENVIRONMENT=Development
dotnet ef database update
```

Here is the command for the PROD environment:
```
export ASPNETCORE_ENVIRONMENT=Production
dotnet ef database update
```

## Adding new migration scripts
```
dotnet ef migrations add NameOfYourMigration
```