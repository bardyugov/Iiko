Creating new ef-core migrations 

```sh
 dotnet ef migrations add {MigrationName} --project Iiko.Infrastructure --startup-project Iiko.Core
```

Applying migrations

```sh
dotnet ef database update --project Iiko.Infrastructure --startup-project Iiko.Core
```

Run without docker 
```sh
dotnet ef database update --project Iiko.Infrastructure --startup-project Iiko.Core
cd ./Iiko.Core
dotnet run 
```

Run with docker
```sh
docker-compose up
```