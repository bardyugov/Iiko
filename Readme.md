Creating new ef-core migrations 

```sh
 dotnet ef migrations add {MigrationName} --project Aiko.Infrastructure --startup-project Aiko.Core
```

Applying migrations

```sh
dotnet ef database update --project Aiko.Infrastructure --startup-project Aiko.Core
```