# Popular Seguros - Sistema de Gestión de Pólizas

Solución Full Stack para la gestión de pólizas de seguros, desarrollada como prueba técnica. Implementa una arquitectura monolítica modular con **.NET 8** para el backend y **React (Vite)** para el frontend.

## 📋 Requisitos Previos

* **Visual Studio 2022** (con carga de trabajo ASP.NET y desarrollo web).
* **SQL Server** (Express o Developer).
* **Node.js** (v18 o superior).
* **.NET SDK 8.0**.

---

## 🚀 Guía de Instalación y Ejecución

Sigue estos pasos en orden para levantar el proyecto localmente:

### 1. Configuración de Base de Datos
1.  Navega a la carpeta `/Database` de este repositorio.
2.  Abre el archivo `Script-Inicial-DB` con **SQL Server Management Studio (SSMS)**.
3.  Ejecuta el script completo. Esto creará la base de datos `PopularSegurosDB`, las tablas y poblará los catálogos necesarios.
    * *Nota: El script inserta un usuario administrador por defecto.*

### 2. Configuración del Backend (.NET)
1.  Abre el archivo `PopularSeguros.sln` con Visual Studio.
2.  En el proyecto **PopularSeguros.API**, abre el archivo `appsettings.json`.
3.  Verifica o ajusta la cadena de conexión `DefaultConnection` para que coincida con tu instancia local de SQL Server:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=PopularSegurosDB;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```
4.  Ejecuta el proyecto (F5 o Ctrl+F5). La API se iniciará (por defecto en `https://localhost:7145`).

### 3. Configuración del Frontend (React)
1.  Abre una terminal (PowerShell o CMD).
2.  Navega a la carpeta del cliente:
    ```bash
    cd popular-seguros-client
    ```
3.  Instala las dependencias:
    ```bash
    npm install
    ```
4.  Inicia el servidor de desarrollo:
    ```bash
    npm run dev
    ```
5.  Abre el navegador en la URL indicada (usualmente `http://localhost:5173`).

---

## 🔐 Credenciales de Acceso

Para ingresar al sistema, utilice las siguientes credenciales de prueba generadas por el script de base de datos:

* **Usuario:** `admin`
* **Contraseña:** `123456`

---

## 🛠️ Stack Tecnológico

* **Backend:** ASP.NET Core Web API 8.0, Entity Framework Core (Database First), LINQ.
* **Frontend:** React 18, Vite, Bootstrap 5, Axios, React Router.
* **Base de Datos:** SQL Server (Normalización, Relaciones, Eliminado Lógico).
* **Seguridad:** Hashing de contraseñas (SHA-256), CORS.

---

## 👤 Autor
Jordan Álvarez González