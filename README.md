Task Manager (.NET Framework)

A simple Task Manager built with ASP.NET Core (.NET 9.0) and Entity Framework Core (EF Core) using SQLite for data storage. Users can add, edit, delete, and view tasks, sorted by due date first, then priority.
Tech Stack
   Backend: ASP.NET Core Minimal API
   Database: SQLite (EF Core)
   Frontend: HTML, CSS, JavaScript
How to Run
   Clone the repository: 
   -> git clone https://github.com/KevinDal2027/TaskManager-.NET-Framework.git
   -> cd TaskManager-.NET-Framework

  Install dependencies: 
   -> dotnet restore

Run the application:

   -> dotnet run
Access the API:
    API: http://localhost:5075/tasks
UI Display:
    -> Use VSC or anything that can run browser with HTML/JS, Open the index.html

API Endpoints
Method
GET	/tasks	Get all tasks
GET	/tasks/{id}	Get task by ID
POST	/tasks	Add new task
PUT	/tasks/{id}	Update a task
DELETE	/tasks/{id}	Delete a task

Prerequisites
    .NET 9.0 SDK
    Git
    A web browser/VSC
