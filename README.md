# Order of things for the database class:
## Create the projects in the solution and add folders:

### Presentation - C# Web API
#### Folders: (Both already exists on creation.)
- Controllers
- Properties

### Data - Class Library
#### Folders:
- Contexts
- Entities
- Interfaces
- Migrations (Will be automatically added when migrating.)
- Repositories
- DataBase

### Business - Class Library
#### Folders:
- Factories
- Interfaces
- Models
- Services

### Domain - Class Library
#### Folders:
- Models (For response/result class)
- Interfaces

## Dependencies:
- WebAPI => Business
- Business => Data
- Data => Domain
- Business => Domain

## Create Entities in the data layer.
## Create DataContext.
## Register entities in the context.
## Create Database in the databse folder.
## Connect the database to the webapi in the api.json file by adding the connectionstring.
## Migrate.
## Doublecheck the migration. Make sure things cascade correctly.
## Update the database.
## Create Result/response class.
## Create a base repository in the data layer.
## Create a interfaces of the base repository.
## Create repositories for all entities.
## Create specific overrides.
## Create interfaces for all repositories.
## Register all the respositories in the program file.
## Create models for all entities that are needed.
## Create forms for the things needed.
## Create factories.
- Entity => Model
- Form => Entity
## Create Services.
## Create interfaces on all services.
## Register all services in the program file.
## Create APIs.

## Create tests. (Install inmemory database)













### Installs in npm package manager:

Install-Package Microsoft.EntityFrameworkCore.Tools

Install-Package Microsoft.EntityFrameworkCore.SqlServer

Install-Package Microsoft.EntityFrameworkCore.Design

#### Add design in both data and presentation layer.

# Versionhandling of database:

### Steps: 

Write in console: (While in the Data-layer)

- Add-Migration "Init"

To migrate the changes. (Write a commit message in the "")

Check so that the migration is correct. (Migration folder should have been created and a new window opened)

Use:

- Remove-Migration

If anything is incorrect. Otherwise:

- Update-Database


# For testing:

Create seperate Xunit projects for repositories and services.


### Structure:

//Arrange

//Act

//Assert


### Install in testing projects:

Install-Package Microsoft.EntityFrameworkCore.InMemory































# Parts:

### Backend:

https://github.com/SimonR-prog/CMS_layer

### FrontEnd(React):

https://github.com/SimonR-prog/Project_Databas

### Sources:

Learning about database handling: 

https://www.youtube.com/watch?v=l4XUngl4pmo&list=PLXkHB_gtvM_VXeWG63TVagE6cBJrx_WV4
