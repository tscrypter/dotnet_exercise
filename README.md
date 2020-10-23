# Dog Daycare  
![.NET Core](https://github.com/tscrypter/dotnet_exercise/workflows/.NET%20Core/badge.svg)  

### Description
Dog Daycare is a simple example application composed of an MVC application and a WebAPI service. Both apps have their own 
MySQL database, communicate with each other using Eureka for service registry, and can be monitored using a Spring Boot Admin. 
All necessary services can be started from the main directory with the following command:  
```bash
docker-compose up && docker-compose down
```


### Notes  
  - Scaffolding for MVC does not work on linux
    - https://github.com/dotnet/Scaffolding/issues/1418
    - https://github.com/dotnet/Scaffolding/issues/1393
    - https://github.com/dotnet/Scaffolding/issues/1384
  - Ambiguous Steeltoe documentation on connectors
    - https://docs.steeltoe.io/api/v3/connectors/mysql.html "Then add a reference to the appropriate Steeltoe connector NuGet package."
