# Xpand

In this repo you can find an app made for the management of planets, called Xpand.
It is composed of:
- frontend: Angular 15 app, found in Xpand.WebApp folder
- backend: 2 microservices (Xpand.CrewsAPI and Xpand.PlanetsAPI) and a gateway API (Xpand.API)

The microservices are both ASP.NET Core Web APIs with connections to its own Microsoft SQLServer database.
The gateway API acts as an orchestrator between the frontend app and the microservices.

# How to get started

- Run the command 'update-database' in both microservices to create the database (including seed data).
- Run the command 'npm install' in the frontend project to install all the dependencies.
- Start the microservices, then the gateway API and then the frontend project.

# Technologies
- .NET 7 with nullable enabled
- CQRS - design pattern applied to the microservices using MediatR
- Entity Framework Core 7 - code first strategy
- AutoMapper - for mapping DTOs
- Swagger - for API visualization and easy testing
- xUnit - for unit testing
- Moq - for mocking
- Angular 15
- Angular Material - for frontend components
