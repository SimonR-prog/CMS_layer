# Projects;

### Data - Class Library
## Folders;
- Contexts
- Entities
- Interfaces
- Migrations (Will be automatically added when migrating.)
- Repositories

### Business - Class Library
## Folders;
- Factories
- Interfaces
- Models
- Services

### Domain - Class Library
## Folders;
- Models (For response/result class)
- Interfaces


### Presentation - C# Web API
## Folders; (Both already exists on creation.)
- Controllers
- Properties

### Installs in npm package manager;

Install-Package Microsoft.EntityFrameworkCore.Tools

Install-Package Microsoft.EntityFrameworkCore.SqlServer

Install-Package Microsoft.EntityFrameworkCore.Design

#### Add design in both data and presentation layer.











# For testing;

Create seperate Xunit projects for repositories and services.


### Structure;

//Arrange

//Act

//Assert


### Install in testing projects;

Install-Package Microsoft.EntityFrameworkCore.InMemory

































### Backend:

https://github.com/SimonR-prog/CMS_layer

### FrontEnd(React):

https://github.com/SimonR-prog/Project_Databas

### Sources for things:

Learning about database handling: 
https://www.youtube.com/watch?v=l4XUngl4pmo&list=PLXkHB_gtvM_VXeWG63TVagE6cBJrx_WV4
