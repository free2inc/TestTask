# TestTask ASP.NET Core MVC Product Management System

This is a simple product management system built using ASP.NET Core MVC. It includes product CRUD operations, and an audit trail for tracking changes to product data.

## Features
- Product management with full CRUD operations for Admin
- Read-only product view for User
- Product audit trail with old and new values logged
- Unit test for VAT calculation
- REST API for accessing audit trail

## Prerequisites
- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 3.1 or later)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or another compatible database)

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/yourusername/ProductManagementSystem.git
cd ProductManagementSystem
```

## Configuration

### Database Connection String

1. Open the `appsettings.json` file located in the root directory of the project.

2. Locate the `"ConnectionStrings"` section:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_username;Password=your_password;"
     }
   }
   ```

### Applying Migrations

Provide instructions for applying migrations to update the database schema:


1. Open a nuget package manager console in Visual Studio 2022.

2. Run the following command to apply pending migrations and update the database:

   ```bash
   update-database
