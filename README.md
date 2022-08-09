# GPM Solution

## Shapes 

To solve the problem, it was decided to implement a .NET application based on a microservices architecture using DDD practices.

### Structure
```
GPM/
├───src/    
│   ├───Application/  --------> Entry point of the application. This layer was implemented with CQRS pattern using MediatR.
│   ├───Domain/  --------> Each shape is represented as a list of vertices. 
│   ├───Infrastructure/  --------> Contains EF Core migrations and logic to persist to database. 
├───tests/
│   ├───ShapeUnitTests/  --------> Contains unit testing 
```

### dotnet
The solution and all projects were created using dotnet from a UNIX environment.

### Entity Framework 6
For convenience it was used as ORM Entity Framework Core 6 (Code First) . Migrations are found in the infrastructure project.

### PostgreSQL
For the local tests, PostgreSQL was used to manage the databases. The connection to the database can be modified through the appsettings.json file.

### Solution 
The proposed solution consists of creating figures through the WebApi using the creation endpoint and then using the other endpoint to request the calculation of the volume of the figures.