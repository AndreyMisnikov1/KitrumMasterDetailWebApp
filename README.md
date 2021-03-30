# MasterDetailWebApp
Master Detail Web App

The solution contains 3 projects:
1) BL - Business Logic layer.
2) DAL - Data Access Layer (EF). 
3) WebApplication - .net core MVC application 

The solution contains the following packages:
1) LinqKit to build expressions for IQueryable<TSource>
2) Automapper to map DTO & EF entities
3) AutoMapper.Extensions.ExpressionMapping to map expressions with DTO to EF entities

Get started guide:
1) Open appsettings.json file and change DatabaseConnection values if it is needed
2) Update database dotnet ef --startup-project ../WebApplication/ database update (run from DAL project)

Note to readers: If you haven't installed dotnet ef, you need to install it first: dotnet tool install --global dotnet-ef

in order to add migration use the following command:
dotnet ef --startup-project ../WebApplication/ migrations add [migrationName]


How to work with
Repository(DAL):
1) add implementation to IGenericRepository, GenericRepository if the database action is common and can be reused.
2) create interface from IGenericRepository<T> and implement new method in own reposity if the action should not be reused
Services(BL):
1) add implementation to IGenericService, GenericDbServiceBase if the database action is common and can be reused.
2) create interface from IGenericService<T> and implement new method in own service if the action should not be reused