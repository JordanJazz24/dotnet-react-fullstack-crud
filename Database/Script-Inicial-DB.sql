/*
 * Script de Inicialización de Base de Datos - Popular Seguros
 * Descripción: Definición de esquemas (DDL) y carga de datos iniciales (DML).
 * Autor: Jordan Álvarez González
 * Fecha: Diciembre 2025
 */

USE PopularSegurosDB;
GO

/* =============================================
   SECCIÓN 1: TABLAS DE CATÁLOGO
   ============================================= */

CREATE TABLE TipoPoliza (
    IdTipoPoliza INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Cobertura (
    IdCobertura INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(200)
);

CREATE TABLE EstadoPoliza (
    IdEstadoPoliza INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL
);

/* =============================================
   SECCIÓN 2: SEGURIDAD Y ACCESO
   ============================================= */

CREATE TABLE Usuario (
    IdUsuario INT PRIMARY KEY IDENTITY(1,1),
    LoginUsuario VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(MAX) NOT NULL, -- SHA256 Hash
    NombreCompleto VARCHAR(100)
);

/* =============================================
   SECCIÓN 3: GESTIÓN DE CLIENTES Y PÓLIZAS
   ============================================= */

CREATE TABLE Cliente (
    Cedula VARCHAR(20) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    PrimerApellido VARCHAR(50) NOT NULL,
    SegundoApellido VARCHAR(50) NOT NULL,
    TipoPersona VARCHAR(20) NOT NULL, -- Valores esperados: 'Fisica', 'Juridica'
    FechaNacimiento DATE NOT NULL
);

CREATE TABLE Poliza (
    NumeroPoliza INT PRIMARY KEY IDENTITY(1,1),
    
    -- Relaciones (Foreign Keys)
    CedulaAsegurado VARCHAR(20) NOT NULL,
    IdTipoPoliza INT NOT NULL,
    IdCobertura INT NOT NULL,
    IdEstadoPoliza INT NOT NULL,
    
    -- Datos Transaccionales
    MontoAsegurado DECIMAL(18,2) NOT NULL,
    Prima DECIMAL(18,2) NOT NULL,
    Aseguradora VARCHAR(100) NOT NULL DEFAULT 'Popular Seguros',
    FechaEmision DATETIME NOT NULL,
    FechaVencimiento DATETIME NOT NULL,
    FechaInclusion DATETIME DEFAULT GETDATE(),
    
    -- Auditoría / Borrado Lógico
    Eliminado BIT DEFAULT 0,

    -- Constraints
    FOREIGN KEY (CedulaAsegurado) REFERENCES Cliente(Cedula),
    FOREIGN KEY (IdTipoPoliza) REFERENCES TipoPoliza(IdTipoPoliza),
    FOREIGN KEY (IdCobertura) REFERENCES Cobertura(IdCobertura),
    FOREIGN KEY (IdEstadoPoliza) REFERENCES EstadoPoliza(IdEstadoPoliza)
);
GO

/* =============================================
   SECCIÓN 4: DATOS SEMILLA (SEED DATA)
   ============================================= */

-- Catálogos
INSERT INTO TipoPoliza (Nombre) VALUES 
('Automóvil'), 
('Vida'), 
('Incendio'), 
('Gastos Médicos');

INSERT INTO Cobertura (Nombre, Descripcion) VALUES 
('Responsabilidad Civil', 'Daños a terceros'),
('Cobertura Completa', 'Incluye colisión y robo'),
('Vida Básico', 'Fallecimiento por cualquier causa');

INSERT INTO EstadoPoliza (Nombre) VALUES 
('Activa'), 
('Vencida'), 
('Suspendida');

-- Usuario Administrador
-- Password Original: "123456" (Almacenado como Hash SHA256)
INSERT INTO Usuario (LoginUsuario, PasswordHash, NombreCompleto) 
VALUES ('admin', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'Administrador del Sistema');

-- Clientes de Prueba
INSERT INTO Cliente (Cedula, Nombre, PrimerApellido, SegundoApellido, TipoPersona, FechaNacimiento)
VALUES 
('101110111', 'Juan', 'Perez', 'Rodriguez', 'Fisica', '1990-05-15'),
('202220222', 'Maria', 'Gomez', 'Salazar', 'Fisica', '1985-10-20');

-- Pólizas de Prueba
INSERT INTO Poliza (CedulaAsegurado, IdTipoPoliza, IdCobertura, IdEstadoPoliza, MontoAsegurado, Prima, Aseguradora, FechaEmision, FechaVencimiento)
VALUES 
('101110111', 1, 2, 1, 5000000.00, 25000.00, 'Popular Seguros', '2023-01-01', '2024-01-01'),
('202220222', 2, 3, 1, 10000000.00, 15000.00, 'Popular Seguros', '2023-06-01', '2024-06-01');

GO