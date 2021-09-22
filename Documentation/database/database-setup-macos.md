# How to setup the database for MacOS
We use a MS SQL database here. Under Windows the database is being started in the background if installed. On Mac you need to start a docker image.

Setup the most recent version of Docker and execute the following commands in a terminal:

```
docker pull mcr.microsoft.com/azure-sql-edge
```

```
docker run -d --name MySQLServer -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=password123' -p 1433:1433 mcr.microsoft.com/azure-sql-edge
```

You will be able to connect to the database with:
- Server: localhost
- User: sa
- Password: password123

If you change anything, then also modify the appseetings.Development.json. It's to have the credentials exposed in the appseetings.Development.json since they are for you local database. But don't expose username + password for the QA od PROD environment!