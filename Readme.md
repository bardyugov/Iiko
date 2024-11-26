Creating new ef-core migrations 

```sh
 dotnet ef migrations add {MigrationName} --project Iiko.Infrastructure --startup-project Iiko.Core
```

Applying migrations

```sh
dotnet ef database update --project Iiko.Infrastructure --startup-project Iiko.Core
```

Run Development Mode 
```sh
dotnet ef database update --project Iiko.Infrastructure --startup-project Iiko.Core
cd ./Iiko.Core
dotnet run 
```

Run Production Mode
```sh
docker-compose up
```