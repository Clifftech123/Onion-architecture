
# Supermarket Web API

Supermarket Web API is a simple RESTful API built with ASP.NET Core and .NET  8, designed to demonstrate how to create RESTful APIs with role-based authentication. The API provides endpoints for managing products, categories, and orders, and is secured using role-based access control.
## Features

- **Role-Based Authentication**: Secure your API endpoints with role-based access control.
- **Docker Support**: Easily deploy and run the API using Docker.
- **Swagger Documentation**: Interactive API documentation for testing and exploration.
- **Entity Framework Core**: Robust data access with support for various databases.
- **AutoMapper**: Simplify object-object mapping with a convention-based approach.


## Role-Based Authentication

Role-based authentication has been added to the API to ensure that only authorized users can access certain endpoints. This feature is implemented using ASP.NET Core Identity and JWT tokens for authentication.



## Getting Started

### Prerequisites

- .NET Core SDK
- Docker (optional)
- Visual Studio (optional)

### Running the API

#### Using Docker

If you have Docker and Visual Studio installed on your machine, you can open the solution file using Visual Studio and run the project using the Docker profile.

#### Using .NET CLI

If not, open the terminal or command prompt at the API root path (`/src/Supermarket.API/`) and run the following commands, in sequence:

```sh
dotnet restore
dotnet run
```



### Testing Role-Based Authentication

To test the role-based authentication, you will need to:

1. **Create Users**: Use the API to create users and assign them to roles.

2. **Obtain Tokens**: Use the login endpoint to obtain a JWT token for a user.

3. **Access Secured Endpoints**: Use the obtained token to access the secured endpoints. Ensure that only users with the correct roles can access the endpoints.

## Contributing

Contributions are welcome! Please read the [contributing guide](CONTRIBUTING.md) for details on how to contribute to this project.

