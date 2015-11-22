# SOA

[![Build status](https://ci.appveyor.com/api/projects/status/ipi2k6mnc96nhni9?svg=true)](https://ci.appveyor.com/project/gmich/soa)

A test project that combines the SOA architectural patterns with REST

##Project structure

* The services constract individual modules.
* All the modules have a consumer project.
* All the consumer ssubscribe using the contracts found in the Bus/Contracts project.
* The Bus/ServiceBus project collects all the modules and runs as a windows service.
* The clients reference the contract project to publish messages. 

##Technologies Used
MassTransit and RabbitMQ for the service bus.
Microsoft ASP.NET MVC for the client project.
