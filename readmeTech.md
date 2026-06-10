Sistema de Gestión de Ficha Clínica Veterinaria (A Domicilio)
📌 Descripción del Proyecto
Este sistema es una solución de software multiplataforma orientada al futuro para la gestión de fichas clínicas veterinarias, diseñada específicamente para un modelo de atención 100% a domicilio. Permite el registro de propietarios, pacientes (mascotas) e historiales médicos en terreno de manera Offline-First mediante almacenamiento local, sincronizándose de forma transparente con un servidor central en la nube.

🛠️ Stack Tecnológico y Herramientas (VS Code Setup)
1. Cliente Multiplataforma (/src/Veterinaria.App)
Framework: .NET MAUI con Blazor Hybrid (HTML5, TailwindCSS/, C#).

Entorno de Ejecución: .NET 9.0.

Base de Datos Local: SQLite (para persistencia 100% en terreno).

ORM: Entity Framework Core 9 (versión SQLite).

2. Servidor Central (/src/Veterinaria.API)
Backend: .NET 9.0 Web API (Arquitectura REST).

Base de Datos Central: PostgreSQL (Hospedado en Linux).

Almacenamiento de Archivos: Object Storage (Compatible con S3 / Cloudinary) para optimizar el almacenamiento de fotos de perfil de las mascotas (~200 KB por imagen).

3. Entorno de Desarrollo (Requisitos en VS Code)
Para desarrollar y compilar este proyecto desde VS Code, se requieren las siguientes herramientas:

SDK de .NET 9.0 (Runtime y SDK global).

Cargas de trabajo de .NET MAUI: Instaladas desde la terminal mediante:

Bash
dotnet workload install maui
Extensiones críticas de VS Code:

ms-dotnettools.csharp (Soporte oficial de C#).

ms-dotnettools.maui (Extensión oficial de .NET MAUI para depuración y selección de dispositivos/emuladores).

ms-dotnettools.vscode-dotnet-runtime (Herramientas de ejecución).

🔐 Matriz de Roles y Reglas de Negocio
Roles del Sistema (RBAC)
Veterinario: Acceso total. Autorizado para cerrar consultas, emitir diagnósticos y recetas médicas.

Supervisor (Asistente en Terreno): Permisos de lectura total, creación y edición de datos demográficos (clientes/mascotas) y llenado preliminar de la consulta (anamnesis, peso). No puede editar diagnósticos firmados.

Administrador: Gestión técnica de usuarios, credenciales, auditoría y mantenimiento del servidor central.

Reglas Críticas del MVP
Identificación Única: Código estandarizado correlativo (Ej: PM-001, PM-002) indexado en la base de datos para búsquedas inmediatas.

Caché de Último Peso: Las variaciones de peso se guardan cronológicamente en cada consulta. La tabla Mascota solo almacena el valor del último peso registrado para acelerar las vistas principales.

Edad Dinámica: Calculada al vuelo en C# comparando FechaNacimiento con la fecha actual (no se guarda en la BD).

Ciclo de Vida de la Consulta: Inicia en estado Borrador (editable por Supervisor/Vet). Al pasar a estado Finalizada/Firmada por el Veterinario, el registro queda blindado contra modificaciones.

📂 Estructura de la Solución (Arquitectura Limpia)
El proyecto se organizará en una solución unificada de .NET para facilitar las referencias locales:

Plaintext
VeterinariaClinica/
│
├── VeterinariaClinica.sln          # Archivo de solución global
│
└── src/
    ├── Veterinaria.Shared/         # Entidades de datos comunes, DTOs y reglas de validación
    │   └── Models/                 # Clases Mascota, Propietario, Consulta, Usuario
    │
    ├── Veterinaria.App/            # Proyecto .NET MAUI Blazor Hybrid (UI Multiplataforma)
    │   ├── Components/             # Vistas e interfaz de usuario en HTML/Razor
    │   ├── Data/                   # DbContext de SQLite local y servicios de sincronización
    │   └── Platforms/              # Configuraciones nativas (Android, Windows)
    │
    └── Veterinaria.API/            # Proyecto .NET 9 Web API (Servidor Linux)
        ├── Controllers/            # Endpoints de la API REST
        └── Data/                   # DbContext de PostgreSQL central
💻 Comandos Útiles para la CLI de .NET (VS Code Terminal)
Gestión de la Solución:
Crear la solución: dotnet new sln -n VeterinariaClinica

Compilar todo el proyecto: dotnet build

Ejecución del Cliente (.NET MAUI):
Ejecutar en Windows Desktop:

Bash
dotnet run --project src/Veterinaria.App/Veterinaria.App.csproj -f net9.0-windows10.0.19041.0
Ejecutar en Android (Emulador/Dispositivo físico):

Bash
dotnet build src/Veterinaria.App/Veterinaria.App.csproj -t:Run -f net9.0-android
Ejecución del Servidor (Web API):
Ejecutar la API localmente:

Bash
dotnet watch --project src/Veterinaria.API/Veterinaria.API.csproj
🔮 Roadmap / Evolución Futura
Fase 2: Módulo de Cronograma, Calendario y optimización de rutas a domicilio.

Fase 3: Planes de Vacunación y Desparasitación con alertas dinámicas.

Fase 4: Control de Pagos, Métodos de Pago en terreno e ingresos mensuales.

Fase 5: Módulo de Control de Inventario de medicamentos e insumos médicos.