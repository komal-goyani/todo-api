# Todo CRUD API with .NET and MongoDB

## Introduction
The core functionality of this application is to create a REST based API that allows users to Create, Retrieve, Update and Delete (CRUD) to-do list items.

## Prerequisites

Before setting up the Todo CRUD API, ensure that you have the following installed on your system:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [MongoDB](https://www.mongodb.com/try/download/community)

## Setup Instructions

Follow these steps to set up and run the Todo CRUD API locally:

1. **Clone the Repository:**
    ```
    https://github.com/komal-goyani/todo-api
    ```

2. **Navigate to the Project Directory:**
    ```
    cd todo-crud-api
    ```

3. **Restore Dependencies:**
    ```
    dotnet restore
    ```

4. **Start MongoDB Service:**
- Start the MongoDB service on your local machine. You can refer to the MongoDB documentation for instructions on how to start the service based on your operating system.

5. **Update MongoDB Connection String:**
- Open the `appsettings.json` file and update the MongoDB connection string for _ToDoDatabase_ with your local MongoDB configuration.

6. **Run the Application:**
    ```
    dotnet run
    ```

7. **Test the API:**
- Once the application is running, you can test the API endpoints using tools like Postman or curl. The base URL for the API will typically be `http://localhost:7150`.

## API Endpoints

The Todo CRUD API provides the following endpoints:

- **GET /api/todo:** Get all todos.
- **GET /api/todo/{id}:** Get a todo by its ID.
- **POST /api/todo:** Create a new todo.
- **PUT /api/todo/{id}:** Update an existing todo.
- **DELETE /api/todo/{id}:** Delete a todo by its ID.

## Technologies and Tools Used

- **.NET 8:** Used as the primary framework for building the API.
- **MongoDB:** Chosen as the backend database for storing todo data.
- **MongoDB.Driver:** .NET driver for interacting with MongoDB.
- **ASP.NET Core Web API:** Used for building RESTful APIs.
- **C# Programming Language:** Used for implementing business logic and API endpoints.
- **Git:** Version control system for managing project source code.
- **Postman:** Used for testing API endpoints during development.
- **Visual Studio:** Text editor used for writing code and managing project files.
