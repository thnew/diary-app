# How to use migrations

## Setting up the entity framework tools

## Updating the database
```
dotnet ef database update -- --environment Development
```

Here are the command for the other environments:
```
dotnet ef database update -- --environment Test
```
```
dotnet ef database update -- --environment Prod
```

## Adding new migration scripts
```
dotnet ef migrations add NameOfYourMigration
```