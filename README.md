# Insurance Policy Management System

A modern full-stack insurance policy management application demonstrating enterprise-level architecture and best practices. Built with **.NET 10 Web API**, **Entity Framework Core**, **React 19**, and **SQL Server**.

## 🚀 Tech Stack

### Backend
- **ASP.NET Core Web API 10.0** - Latest .NET framework
- **Entity Framework Core 10.0** - ORM with Database-First approach
- **SQL Server** - Relational database with normalization
- **LINQ** - Data querying and manipulation

### Frontend
- **React 19** - Modern UI library
- **Vite 7** - Fast build tool and dev server
- **Bootstrap 5** - Responsive UI framework
- **Axios** - HTTP client
- **React Router 7** - Client-side routing

### Architecture & Patterns
- RESTful API design
- DTO (Data Transfer Objects) pattern
- Dependency Injection
- Soft Delete implementation
- CORS configuration
- Entity relationships and navigation properties

## ✨ Features

- ✅ Complete CRUD operations for insurance policies
- ✅ Client management system
- ✅ Policy type and coverage catalogs
- ✅ Policy status tracking
- ✅ Authentication system
- ✅ Soft delete (logical deletion)
- ✅ Date validation and business rules
- ✅ Responsive user interface
- ✅ RESTful API endpoints

## 📋 Prerequisites

Before running this project, ensure you have the following installed:

- **Visual Studio 2022** (with ASP.NET and web development workload)
- **SQL Server** (Express or Developer edition)
- **Node.js** (v18 or higher)
- **.NET SDK 10.0**
- **SQL Server Management Studio (SSMS)** (recommended)

## 🛠️ Installation & Setup

Follow these steps to run the project locally:

### 1. Database Configuration

1. Navigate to the `/Database` folder in this repository
2. Open the `Script-Inicial-DB` file with **SQL Server Management Studio (SSMS)**
3. Execute the complete script. This will:
   - Create the `PopularSegurosDB` database
   - Create all necessary tables
   - Set up relationships and constraints
   - Populate catalog data
   - Insert a default admin user

### 2. Backend Setup (.NET API)

1. Open the `PopularSeguros.sln` file with Visual Studio 2022
2. In the **PopularSeguros.API** project, open the `appsettings.json` file
3. Update the connection string to match your local SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=PopularSegurosDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

4. Build the solution (Ctrl+Shift+B)
5. Run the project (F5 or Ctrl+F5)
   - The API will start on `https://localhost:7145` by default
   - Swagger/OpenAPI documentation available at `/swagger` endpoint

### 3. Frontend Setup (React)

1. Open a terminal (PowerShell, CMD, or your preferred terminal)
2. Navigate to the client folder:

```bash
cd popular-seguros-client
```

3. Install dependencies:

```bash
npm install
```

4. Start the development server:

```bash
npm run dev
```

5. Open your browser at the URL shown in the terminal (usually `http://localhost:5173`)

## 🔐 Default Credentials

Use these credentials to log into the system (created by the database script):

- **Username:** `admin`
- **Password:** `123456`

## 📁 Project Structure

```
PopularSeguros/
├── Database/                    # SQL scripts
│   └── Script-Inicial-DB       # Initial database setup
├── PopularSeguros.API/         # Backend (.NET)
│   ├── Controllers/            # API endpoints
│   ├── Data/                   # DbContext
│   ├── Models/                 # Entity models
│   ├── DTOs/                   # Data transfer objects
│   └── appsettings.json        # Configuration
├── popular-seguros-client/     # Frontend (React)
│   ├── src/
│   │   ├── components/         # React components
│   │   ├── pages/              # Page components
│   │   └── services/           # API services
│   └── package.json
└── README.md
```

## 🔌 API Endpoints

### Authentication
- `POST /api/Auth/Login` - User authentication

### Policies
- `GET /api/Polizas` - Get all policies
- `GET /api/Polizas/{id}` - Get policy by ID
- `POST /api/Polizas` - Create new policy
- `PUT /api/Polizas/{id}` - Update policy
- `DELETE /api/Polizas/{id}` - Delete policy (soft delete)

### Catalogs
- `GET /api/Catalogos/Tipos` - Get policy types
- `GET /api/Catalogos/Coberturas` - Get coverages
- `GET /api/Catalogos/Estados` - Get policy statuses
- `GET /api/Catalogos/Clientes?cedula={id}` - Search client by ID

## 🗄️ Database Schema

The system manages the following main entities:

- **Cliente** (Client) - Customer information
- **Poliza** (Policy) - Insurance policies
- **TipoPoliza** (Policy Type) - Catalog of policy types
- **Cobertura** (Coverage) - Insurance coverage catalog
- **EstadoPoliza** (Policy Status) - Policy status catalog
- **Usuario** (User) - System users

All relationships are properly configured with foreign keys and navigation properties.

## 👨‍💻 Author

**Jordan Álvarez González**
