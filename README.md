# Flower Commerce API Documentation

## Overview

The **Flower Commerce API** is a backend application built with **ASP.NET Core** for handling product and category management in a flower e-commerce platform. It includes features for authentication, user management, and JWT-based authorization to secure API endpoints. The API connects to a SQL Server database using Entity Framework Core.

## Features

- **User Authentication**: Using JWT tokens for user login and role-based access control.
- **Admin and User Roles**: Two types of user roles (Admin and User) with different access rights.
- **Product Management**: Endpoints for managing flower products (CRUD operations).
- **Swagger UI**: Automatically generated API documentation with interactive features for testing API endpoints.
- **Password Hashing**: Secure password hashing and verification using `PasswordHasher`.
- **Authorization Policies**: Restrict access to certain endpoints based on roles.

## Getting Started

### Prerequisites

- **.NET 6.0 or later**: Make sure you have the latest .NET SDK installed.
- **SQL Server**: A SQL Server instance for the database.
- **Postman / Swagger UI**: For testing API endpoints.

### Setup

1. Clone this repository:

   ```bash
   git clone https://github.com/your-repository/flower-commerce-api.git
   cd flower-commerce-api
   ```

2. Install the required dependencies using .NET CLI:

   ```bash
   dotnet restore
   ```

3. Configure the `appsettings.json` file with your SQL Server connection string:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your-server;Database=FlowerCommerceDB;User Id=your-username;Password=your-password;"
     },
     "JwtSettings": {
       "Issuer": "your-issuer",
       "Audience": "your-audience",
       "SecretKey": "your-secret-key"
     }
   }
   ```

4. Apply the migrations to set up the database:

   ```bash
   dotnet ef database update
   ```

5. Run the application:

   ```bash
   dotnet run
   ```

### Accessing the API

Once the application is running, the API will be accessible at:

```
http://localhost:5000
```

You can access the API documentation and test endpoints using **Swagger UI** at:

```
http://localhost:5000/swagger
```

### API Endpoints

#### 1. Authentication and Authorization

- **POST /api/auth/login**: Logs in a user and generates a JWT token.

  - Request body:
    ```json
    {
      "username": "user@example.com",
      "password": "yourpassword"
    }
    ```
  - Response:
    ```json
    {
      "token": "your-jwt-token"
    }
    ```

- **POST /api/auth/register**: Registers a new user.

#### 2. Product Management

- **GET /api/products**: Retrieves all products.
- **GET /api/products/{id}**: Retrieves a specific product by its ID.
- **POST /api/products**: Creates a new product (Admin only).
- **PUT /api/products/{id}**: Updates a product (Admin only).
- **DELETE /api/products/{id}**: Deletes a product (Admin only).

#### 3. Role-Based Access

- **Admin Role**: Can create, update, and delete products.
- **User Role**: Can only view products.

## Code Structure

### Program.cs

- Configures services for **JWT Authentication**, **Swagger UI**, **Authorization Policies**, and **Entity Framework Core** for database access.
- Sets up two policies for **Admin** and **User** roles, restricting certain actions to admins only.

### Services

- **PasswordService**: Handles password hashing and verification using `PasswordHasher<User>`.
- **JwtService**: Manages the creation and validation of JWT tokens.

### Security

- The API uses **JWT tokens** for authentication. Tokens are validated on each request using the `JwtBearer` authentication scheme.
- **Role-Based Authorization** is implemented using policies that check the user's role before granting access to certain endpoints.

### Swagger

- **Swagger UI** is enabled in the development environment for API documentation and testing.
- The Swagger UI requires a **Bearer token** for authenticated routes.

### Database

- The database schema is managed using **Entity Framework Core**. The application uses **SQL Server** as the database provider.
- Database models include `User` and `Product` entities, and the database is initialized on startup through migrations.

## Authentication Flow

1. **Login**: The user submits their username and password to the `/api/auth/login` endpoint.
2. **JWT Generation**: If the credentials are valid, a JWT token is generated and returned.
3. **API Access**: The token is used to authenticate requests to protected API endpoints (such as creating or deleting products).

## Security Considerations

- Passwords are never stored in plain text. They are securely hashed using the `PasswordHasher<User>` service.
- JWT tokens are signed using a secret key to ensure integrity and prevent tampering.

## Contributing

If you want to contribute to this project, feel free to fork the repository, create a branch, and submit a pull request with your changes. Please make sure to follow the coding conventions and write unit tests for any new features or bug fixes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
