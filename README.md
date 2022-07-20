# EmployeeDepartmentSearch
ASP.net MVC DbContext Relational MsSql Server Client no JS

## Setup Database
```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Chheangmai@443" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

## Setup Code Dependencies
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package System.ComponentModel.Annotations
```

## Setup Code Database Migration
```bash
dotnet ef migrations add InitialMigrations
dotnet ef database update
```

## Start Code
```bash
dotnet watch run
```

## Clean Up Migration
```bash
dotnet ef database update 0
dotnet ef migrations remove
```

## Version

| Application | Version |
| ------------| --------|
| dotnet      | 6.0.106 |
| dotnet ef   |  6.0.7  |
| docker      | 20.10.17|
| mssql       | 2022prev|
