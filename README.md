# FaithfulRemindersWeb

I'm taking the Average ToDoList Project to the next level by building my own ASP.NET Web API from the Ground Up.

It consists of five main components: Business, Entity, Global, Tests, and API.

When the API is Completed, I will continue Building the FaithfulReminders Maui Application that will be use as the Main User Interface for Consuming this API. 

## Project Structure

### Entity
The `Entity` project contains the Entity Objects that represent tables in the database. 
- These entities are used to map the database tables to object-oriented entities in the program.
- When using Entity Framework Core's Code First approach, you define your entity objects as classes, and the framework automatically creates the corresponding database tables based on these classes. 
- Each entity object represents a single table in the database.

### Business
The `Business` project contains the business logic of the application. It includes the following components:

- **FaithfulDbContext:** The Business project includes a FaithfulDbContext class that represents the database context. This class is responsible for defining the database schema, configuring the entity mappings, and managing the interactions with the database. It sets up the DbSets for the entity types and provides the necessary configuration for Entity Framework Core.

- **Migrations:** Migrations are used to evolve the database schema over time as the application evolves. It allows you to easily apply changes to the database schema or roll back to previous versions. Using Entity Framework Core's migration tools, I can generate migration scripts based on the changes I have made to the DbContext and apply those migrations to the database.

- **Generic BaseRepository:** A generic base repository class that provides basic CRUD operations for interacting with the database using Entity Framework Core. This repository serves as a base class for specific repositories.

- **ToDoItemRepository :** A repository class that inherits from the BaseRepository and provides additional implementation specific for the ToDoItem entity.

- **UserRepository :** Similar to the ToDoItemRepository, this repository inherits from the BaseRepository and provides specific implementation for the User entity.

- **ToDoItemBL :** A Business Logic class that uses the ToDoItemRepository injected through its constructor to interact with the database and perform operations related to TodoItems. It does not handle the dependency injection itself but relies on the injection provided by the DI container. The ToDoItemBL handles business logic, validation, and uses AutoMapper for mapping between entity objects and DTOs (Data Transfer Objects).

- **UserBL :** Similar to the ToDoItemBL, this service class uses the UserRepository injected through its constructor to interact with the database and handle operations related to the User. It also relies on dependency injection for the repository and takes care of business logic, validation, and object mapping. The mapping profiles and validations are set up in the Business project.

- **Mapping Profiles:** The Business project contains mapping profiles that define how entity objects are mapped to DTOs and vice versa using AutoMapper. These profiles help streamline the mapping process and ensure consistent mappings between different object types.

- **Validations:** The Business project also includes validation classes that utilize the Fluent Validations library. These validation classes define the validation rules and logic for the DTO objects and ensure that the data meets the required business rules before being processed by the services.

### Tests
The `Tests` project contains unit tests for the services in the application. These tests ensure that the business logic is functioning correctly and provide a safety net for future changes and refactoring. More Information Coming

### Global Project
The `Global` project serves as a central place for common files used across the application. It includes the following components:

- **Constants Class:** A Constants class that contains strings used across the application. This allows for centralized management and easy access to shared values.
  
## Technologies Used

- **.NET 8:** The project is built using the .NET 8 framework, which provides the latest features and improvements for developing robust applications.
- **Entity Framework Core:** Entity Framework Core is used as the Object-Relational Mapping (ORM) framework to interact with the database. It simplifies data access and provides powerful querying capabilities.
- **MS Test:** The unit tests are written using the MS Test framework, which is a testing framework included with the .NET platform.
- **Fluent Validations:** Fluent Validations library is used for validating input data and enforcing business rules in a fluent and customizable way.
- **AutoMapper:** AutoMapper is employed for mapping between different object types, such as mapping entity objects to DTOs and vice versa. It simplifies the mapping process and reduces boilerplate code.
- **Serilog** an open-source logging library for .NET applications. It provides a flexible and efficient logging framework with a focus on structured logging.
