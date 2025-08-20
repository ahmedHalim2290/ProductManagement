# Product Management API

A robust and scalable RESTful API built with ASP.NET Core for managing a catalog of products. This API supports full CRUD (Create, Read, Update, Delete) operations and is designed to be the backend for a modern single-page application (SPA).

## 🚀 Features

- **RESTful Design**: Clean and predictable API endpoints.
- **Entity Framework Core**: Code-first approach with SQL Server.
- **Dependency Injection**: Built-in IoC container for loose coupling and testability.
- **Data Transfer Objects (DTOs)**: Uses DTOs to shape data and separate the API layer from the domain models.
- **CORS Enabled**: Configured for cross-origin requests to work with frontend clients.
- **Swagger/OpenAPI**: Interactive API documentation at runtime.

## 🛠️ Built With

- [ASP.NET Core 8.0+](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core 9.0](https://docs.microsoft.com/en-us/ef/core/) - ORM
- [SQL Server](https://www.microsoft.com/en-us/sql-server) - Database
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - Swagger documentation
- [AutoMapper](https://automapper.org/) - Object-object mapping (if used)



## 🏁 Getting Started

Follow these instructions to get a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express Edition is sufficient) or use LocalDB which comes with Visual Studio.
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/), [Visual Studio Code](https://code.visualstudio.com/), or [Rider](https://www.jetbrains.com/rider/).

### Installation & Setup

1.  **Clone the Repository**
    ```bash
    git clone https://github.com/ahmedHalim2290/product-management-api.git
    cd product-management-api
    ```

2.  **Database Setup**
    - Update the connection string in `appsettings.json` to point to your local SQL Server instance.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=.;Database=ProductManagementDb;Trusted_Connection=true;MultipleActiveResultSets=true"
    }
    ```

3.  **Apply Database Migrations (Code-First)**
    Open a terminal in the project directory and run the following commands to create the database:
    ```bash
    # This creates a migration (if you haven't already committed them)
    # dotnet ef migrations add InitialCreate

    # This applies the migration to the database
    dotnet ef database update

    # Alternatively, run the application and ensure the database is created through code (if configured).
    ```

4.  **Restore and Run**
    ```bash
    # Restore NuGet packages
    dotnet restore

    # Run the application
    dotnet run
    ```
    The API will start, typically on `https://localhost:7036` or  `http://localhost:5030`   (ports may vary, check your `launchSettings.json`).

5.  **View Swagger Documentation**
    Once the application is running, navigate to `/swagger` in your browser (e.g., `https://localhost:7036/swagger`) to view the interactive API documentation and test the endpoints.

## 📡 API Endpoints

| Method | Endpoint | Description |
| :--- | :--- | :--- |
| `GET` | `/api/Product` | Get all products |
| `GET` | `/api/Product/{id}` | Get a specific product by ID |
| `POST` | `/api/Product` | Create a new product |
| `PUT` | `/api/Product/{id}` | Update an existing product |
| `DELETE` | `/api/Product/{id}` | Delete a product |

**Example Product Object:**
```
{
  "id": 0,
  "name": "string",
  "quantityPerUnit": 1,
  "reorderLevel": 2147483647,
  "supplierId": 0,
  "unitPrice": 0,
  "unitsInStock": 2147483647,
  "unitsOnOrder": 2147483647
}
