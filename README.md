# 🏡 Villa Management API

A comprehensive RESTful API built with ASP.NET Core for managing villa rentals, bookings, and user authentication. This project implements a complete backend solution with JWT authentication, role-based authorization, and integration with PostgreSQL database.

## 📋 Table of Contents

- [Features](#-features)
- [Technologies](#-technologies)
- [Architecture](#-architecture)
- [Getting Started](#-getting-started)
- [Configuration](#-configuration)
- [API Endpoints](#-api-endpoints)
- [Authentication & Authorization](#-authentication--authorization)
- [Database Schema](#-database-schema)
- [Project Structure](#-project-structure)
- [Contributing](#-contributing)
- [License](#-license)

## ✨ Features

### Core Functionality
- **Villa Management**: Complete CRUD operations for villa listings
- **User Authentication**: Secure registration and login with JWT tokens
- **Role-Based Authorization**: Admin and customer role management
- **RESTful API**: Clean and consistent API design
- **API Versioning**: Support for multiple API versions
- **AutoMapper Integration**: Efficient object-to-object mapping
- **Repository Pattern**: Clean separation of data access logic

### Security Features
- JWT (JSON Web Token) authentication
- Password hashing with ASP.NET Core Identity
- Role-based access control (Admin/Customer)
- Secure token generation and validation

### Additional Features
- Comprehensive error handling
- Standardized API responses
- PostgreSQL database integration
- Entity Framework Core for data access
- Swagger/OpenAPI documentation

## 🛠 Technologies

### Backend
- **Framework**: ASP.NET Core 8.0
- **Language**: C# 12
- **ORM**: Entity Framework Core
- **Database**: PostgreSQL
- **Authentication**: ASP.NET Core Identity + JWT

### Libraries & Packages
- **AutoMapper** - Object-to-object mapping
- **Npgsql.EntityFrameworkCore.PostgreSQL** - PostgreSQL provider
- **Microsoft.AspNetCore.Identity.EntityFrameworkCore** - Identity system
- **Microsoft.IdentityModel.Tokens** - JWT token handling
- **System.IdentityModel.Tokens.Jwt** - JWT generation

### Tools
- **Swagger/OpenAPI** - API documentation
- **Visual Studio 2022** - IDE

## 🏗 Architecture

This project follows Clean Architecture principles with clear separation of concerns:

```
Villaa/
│
├── Magic_villa/              # Main API Project
│   ├── Controllers/          # API Controllers
│   ├── Data/                 # DbContext and migrations
│   ├── Model/                # Domain models and DTOs
│   │   └── Dto/             # Data Transfer Objects
│   ├── Repository/           # Data access layer
│   │   └── IRepository/     # Repository interfaces
│   └── Program.cs           # Application entry point
│
├── Magic_Villa_Web/          # Web Client (if applicable)
│
└── Utility/                  # Shared utilities and helpers
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/) (version 12 or higher)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Ibrahim-Omar-dev/Villaa.git
   cd Villaa
   ```

2. **Set up PostgreSQL Database**
   ```sql
   CREATE DATABASE VillaDB;
   ```

3. **Update Connection String**
   
   Edit `appsettings.json` in the `Magic_villa` project:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=VillaDB;Username=your_username;Password=your_password"
     },
     "Token": {
       "Key": "your-secret-key-here-make-it-long-and-secure",
       "Issuer": "https://localhost:7001",
       "Audience": "https://localhost:7001"
     }
   }
   ```

4. **Install Dependencies**
   ```bash
   cd Magic_villa
   dotnet restore
   ```

5. **Apply Database Migrations**
   ```bash
   dotnet ef database update
   ```

6. **Run the Application**
   ```bash
   dotnet run
   ```

   The API will be available at:
   - HTTPS: `https://localhost:7001`
   - HTTP: `http://localhost:5000`
   - Swagger UI: `https://localhost:7001/swagger`

## ⚙️ Configuration

### appsettings.json Structure

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=VillaDB;Username=postgres;Password=yourpassword"
  },
  "Token": {
    "Key": "YourSuperSecretKeyHereMakeItLongAndSecure123456789",
    "Issuer": "https://localhost:7001",
    "Audience": "https://localhost:7001"
  }
}
```

### Environment Variables (Optional)

You can also use environment variables for sensitive data:

```bash
export ConnectionStrings__DefaultConnection="Host=localhost;Database=VillaDB;..."
export Token__Key="your-secret-key"
```

## 📡 API Endpoints

### Base URL
```
https://localhost:7001/api/v1
```

### Authentication Endpoints

#### Register New User
```http
POST /api/v1/User/register
Content-Type: application/json

{
  "userName": "john_doe",
  "email": "john@example.com",
  "name": "John Doe",
  "password": "SecurePassword123!",
  "role": "customer"
}
```

**Response:**
```json
{
  "statusCodes": 200,
  "isSuccess": true,
  "errorMessage": "",
  "result": {
    "id": "user-id-here",
    "userName": "john_doe",
    "email": "john@example.com",
    "name": "John Doe"
  }
}
```

#### Login
```http
POST /api/v1/User/login
Content-Type: application/json

{
  "userName": "john_doe",
  "password": "SecurePassword123!"
}
```

**Response:**
```json
{
  "statusCodes": 200,
  "isSuccess": true,
  "errorMessage": "",
  "result": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "user": {
      "id": "user-id-here",
      "userName": "john_doe",
      "email": "john@example.com",
      "name": "John Doe"
    }
  }
}
```

### Villa Endpoints

#### Get All Villas
```http
GET /api/v1/Villa
Authorization: Bearer {token}
```

#### Get Villa by ID
```http
GET /api/v1/Villa/{id}
Authorization: Bearer {token}
```

#### Create Villa (Admin Only)
```http
POST /api/v1/Villa
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Luxury Beach Villa",
  "details": "Beautiful villa with ocean view",
  "rate": 500.00,
  "occupancy": 6,
  "sqft": 2500,
  "amenity": "Pool, WiFi, Kitchen"
}
```

#### Update Villa (Admin Only)
```http
PUT /api/v1/Villa/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "Updated Villa Name",
  "details": "Updated description",
  "rate": 550.00,
  "occupancy": 8,
  "sqft": 3000,
  "amenity": "Pool, WiFi, Kitchen, Gym"
}
```

#### Delete Villa (Admin Only)
```http
DELETE /api/v1/Villa/{id}
Authorization: Bearer {token}
```

### Standard API Response Format

All endpoints return responses in the following format:

```json
{
  "statusCodes": 200,
  "isSuccess": true,
  "errorMessage": "",
  "result": { }
}
```

## 🔐 Authentication & Authorization

### JWT Token

The API uses JWT (JSON Web Tokens) for authentication. After successful login, you'll receive a token that must be included in subsequent requests.

**Token Structure:**
- **Claims**: User ID, Username, Role
- **Expiration**: 7 days
- **Algorithm**: HMAC-SHA256

### Using the Token

Include the token in the Authorization header:

```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### Roles

The system supports two roles:

1. **Admin**
   - Full access to all endpoints
   - Can create, update, and delete villas
   - Can manage users

2. **Customer**
   - Read-only access to villas
   - Can view villa listings
   - Can make bookings (if implemented)

### Protecting Endpoints

Controllers use the `[Authorize]` attribute:

```csharp
[Authorize(Roles = "admin")]  // Admin only
[Authorize]                    // Any authenticated user
```

## 🗄️ Database Schema

### AppUser Table (ASP.NET Identity)
```sql
- Id (PK)
- UserName
- Email
- Name
- PasswordHash
- SecurityStamp
- ... (other Identity fields)
```

### Villa Table
```sql
- Id (PK)
- Name
- Details
- Rate
- Occupancy
- Sqft
- Amenity
- ImageUrl
- CreatedDate
- UpdatedDate
```

### Roles Table (ASP.NET Identity)
```sql
- Id (PK)
- Name (admin, customer)
- NormalizedName
- ConcurrencyStamp
```

## 📁 Project Structure

```
Magic_villa/
│
├── Controllers/
│   ├── UserController.cs        # Authentication endpoints
│   └── VillaController.cs       # Villa CRUD endpoints
│
├── Data/
│   └── AppDbContext.cs          # EF Core DbContext
│
├── Model/
│   ├── AppUser.cs               # User entity
│   ├── Villa.cs                 # Villa entity
│   ├── ApiResponse.cs           # Standard response model
│   └── Dto/
│       ├── UserDto.cs
│       ├── RequestLoginDto.cs
│       ├── RequestRegistrationDto.cs
│       ├── ResponseLoginDto.cs
│       └── VillaDto.cs
│
├── Repository/
│   ├── IRepository/
│   │   ├── IUserRepository.cs
│   │   └── IVillaRepository.cs
│   ├── UserRepository.cs
│   └── VillaRepository.cs
│
├── Migrations/                   # EF Core migrations
│
├── appsettings.json             # Configuration
├── appsettings.Development.json # Dev configuration
└── Program.cs                   # App startup
```

## 🔧 Common Issues & Solutions

### 1. PostgreSQL Connection Error

**Problem:** Can't connect to PostgreSQL

**Solution:**
- Verify PostgreSQL is running: `sudo service postgresql status`
- Check connection string in `appsettings.json`
- Ensure database exists: `CREATE DATABASE VillaDB;`

### 2. Column Case Sensitivity Error

**Problem:** `column a.Name does not exist`

**Solution:**
Add to your `Program.cs`:
```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
           .UseSnakeCaseNamingConvention());
```

Install package:
```bash
dotnet add package EFCore.NamingConventions
```

### 3. JWT Secret Key Missing

**Problem:** `Token:Key configuration is missing`

**Solution:**
Add to `appsettings.json`:
```json
{
  "Token": {
    "Key": "YourSuperSecretKeyMustBeAtLeast32CharactersLong123456"
  }
}
```

### 4. Migration Errors

**Problem:** Migrations fail to apply

**Solution:**
```bash
# Remove existing migrations
dotnet ef migrations remove

# Create new migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

## 🧪 Testing the API

### Using Swagger UI

1. Navigate to `https://localhost:7001/swagger`
2. Register a new user via `/api/v1/User/register`
3. Login via `/api/v1/User/login` and copy the token
4. Click "Authorize" button and paste: `Bearer {your-token}`
5. Test other endpoints

### Using Postman

1. **Import Collection**: Create a new collection in Postman
2. **Set Variables**:
   - `baseUrl`: `https://localhost:7001/api/v1`
   - `token`: (will be set after login)
3. **Register**: POST to `{{baseUrl}}/User/register`
4. **Login**: POST to `{{baseUrl}}/User/login`
5. **Save Token**: Extract token from login response
6. **Test Endpoints**: Use `Bearer {{token}}` in Authorization header

### Using cURL

**Register:**
```bash
curl -X POST https://localhost:7001/api/v1/User/register \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "testuser",
    "email": "test@example.com",
    "name": "Test User",
    "password": "Test123!",
    "role": "customer"
  }'
```

**Login:**
```bash
curl -X POST https://localhost:7001/api/v1/User/login \
  -H "Content-Type: application/json" \
  -d '{
    "userName": "testuser",
    "password": "Test123!"
  }'
```

**Get Villas:**
```bash
curl -X GET https://localhost:7001/api/v1/Villa \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

## 🚀 Deployment

### Deploy to Azure

1. **Create Azure App Service**
2. **Create Azure PostgreSQL Database**
3. **Update Connection Strings** in Azure Portal
4. **Deploy via Visual Studio** or Azure CLI

### Deploy to AWS

1. **Create EC2 Instance** or use **Elastic Beanstalk**
2. **Set up RDS PostgreSQL**
3. **Configure environment variables**
4. **Deploy application**

Build and run:
```bash
docker build -t villa-api .
docker run -p 8080:80 villa-api
```

## 📚 Learning Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/aspnet/core)
- [Entity Framework Core](https://docs.microsoft.com/ef/core)
- [JWT Authentication](https://jwt.io/introduction)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)
- [AutoMapper](https://automapper.org/)

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Coding Standards

- Follow C# naming conventions
- Use async/await for all I/O operations
- Write meaningful commit messages
- Add XML documentation for public methods
- Include unit tests for new features

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Ibrahim Omar**
- GitHub: [@Ibrahim-Omar-dev](https://github.com/Ibrahim-Omar-dev)

## 🙏 Acknowledgments

- ASP.NET Core Team for the excellent framework
- PostgreSQL Community
- AutoMapper contributors
- All open-source contributors

## 📧 Support

For support and questions:
- Open an issue on GitHub
- Email: [your-email@example.com]

---

**⭐ If you find this project useful, please consider giving it a star!**

## 🔄 Version History

- **v1.0.0** (2026-01-30)
  - Initial release
  - User authentication with JWT
  - Villa CRUD operations
  - Role-based authorization
  - PostgreSQL integration

---

Made with ❤️ by Ibrahim Omar
