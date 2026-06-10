Sistema de Gestión de Ficha Clínica Veterinaria (A Domicilio)
📌 Descripción del Proyecto
Este sistema es una solución de software orientada al futuro para la gestión de fichas clínicas veterinarias, diseñada específicamente para un modelo de atención 100% a domicilio. Permite el registro de propietarios, pacientes (mascotas) e historiales médicos en terreno sin dependencia absoluta de conectividad a internet, garantizando la persistencia local y la sincronización posterior con un servidor central.

El sistema se compone de dos grandes módulos:

Cliente Multiplataforma (.NET MAUI Blazor Hybrid): Una única aplicación responsiva que corre en PC, Tablets y Teléfonos Android.

Servidor Central (.NET 9 Web API): Una API REST hospedada en un entorno Linux económico (DigitalOcean Droplet) que centraliza la información y gestiona el almacenamiento de imágenes.

🛠️ Arquitectura Tecnológica
Cliente (Dispositivo Móvil / PC)
Framework: .NET MAUI con Blazor Hybrid (HTML5, CSS3, C#).

Base de Datos Local: SQLite (Arquitectura Offline-First).

ORM: Entity Framework Core.

Servidor (Nube)
Entorno: VPS Linux (DigitalOcean Droplet ~ $4 USD/mes).

Backend: .NET 9 Web API.

Base de Datos Central: PostgreSQL.

Almacenamiento de Archivos (Fotos de Mascotas): Object Storage (Compatible con AWS S3 / Cloudinary) para optimizar el espacio en el disco del servidor.

📋 Reglas de Negocio Específicas
Identificación Única: Cada paciente poseerá un código estandarizado correlativo (Ej: PM-001, PM-002) indexado para agilizar las búsquedas rápidas en terreno.

Control de Peso Eficiente: * El historial completo de variaciones de peso se almacena cronológicamente en cada consulta médica.

La tabla del paciente almacena únicamente el valor del último peso registrado a modo de caché, optimizando la carga de las pantallas principales.

Cálculo de Edad Dinámico: La edad del paciente no se almacena en la base de datos; se calcula en tiempo de ejecución comparando la FechaNacimiento con la fecha actual.

Estado Reproductivo: Control estricto del estado de esterilización mediante campos booleanos nativos.

Manejo de Imágenes: Las fotografías de perfil de las mascotas tomadas desde el dispositivo serán comprimidas localmente (peso objetivo ~200 KB) antes de ser subidas al Object Storage para resguardar el consumo de datos móviles y almacenamiento.

🔄 Flujo de Sincronización de Datos
El sistema opera bajo una política de resiliencia de red híbrida basada en un único dispositivo activo en terreno:

[ Registro de Consulta ] 
          │
          ▼
[ Guardado en SQLite Local ] (Éxito garantizado 100% offline)
          │
          ├──► (¿Hay Internet Móvil?) ──► SÍ ──► Envía a Web API en tiempo real
          │
          └──► (¿Sin Señal / Rural?) ──► NO  ──► Setea 'TieneCambiosPendientes = true'
                                                       │
                                                       ▼
                                            [ Al detectar Wi-Fi del hogar ]
                                                       │
                                                       ▼
                                            Sincronización Automática de Fondo
🗃️ Modelo de Datos Inicial (Entidades Core)
Propietario
Id (int, PK)

Rut (string, Unique)

NombreCompleto (string)

Telefono (string)

Direccion (string)

Mascota
Id (int, PK)

CodigoEstandarizado (string, Indexed)

Nombre (string)

Especie (string)

FechaNacimiento (DateTime)

EstaEsterilizado (bool)

UltimoPeso (double)

PropietarioId (int, FK)

Propiedad Calculada: Edad (int)

ConsultaClinica
Id (int, PK)

MascotaId (int, FK)

Fecha (DateTime)

PesoRegistro (double)

Anamnesis (string)

Diagnostico (string)

FotoPerfilUrl (string, Nullable)



#FUTURO
## 🔮 Roadmap / Evolución Futura
El sistema está diseñado bajo una arquitectura modular y escalable, lo que permitirá integrar las siguientes fases de desarrollo una vez consolidada la gestión de la ficha clínica core:

* **Módulo de Cronograma y Calendario:** Gestión de citas, rutas optimizadas para visitas a domicilio y recordatorios automáticos de control.
* **Planes de Vacunación y Desparasitación:** Alertas dinámicas de próximas dosis basadas en la especie y edad del paciente.
* **Módulo Financiero y Control de Pagos:** Registro de boletas, métodos de pago recibidos en terreno y reportería de ingresos mensuales.
* **Control de Inventario Local:** Gestión de stock de medicamentos, insumos médicos utilizados en cada consulta y alertas de stock crítico en bodega central (PC).

## 🔐 Gestión de Roles y Seguridad (RBAC)

El sistema controlará el acceso a las funciones mediante tres roles definidos:
1. **Veterinario:** Acceso total al sistema. Autorizado para cerrar consultas, emitir diagnósticos y recetas.
2. **Supervisor:** Perfil orientado al asistente en terreno. Permisos de lectura total, creación y edición de datos demográficos (clientes/mascotas) y llenado de datos preliminares de consulta (anamnesis, peso). No puede editar diagnósticos firmados.
3. **Administrador:** Gestión de usuarios, credenciales, configuración del sistema y mantenimiento de la base de datos central.

## 📋 Reglas de Negocio Adicionales (Vitales)
* **Ciclo de Vida de la Consulta:** Las consultas médicas iniciadas en terreno tendrán el estado de `Borrador`. Solo el usuario con rol `Veterinario` podrá cambiar el estado a `Finalizada/Firmada`, bloqueando cualquier modificación posterior por integridad médica.
* **Trazabilidad:** Cada registro clínico almacenará el ID del usuario que lo creó y la marca de tiempo exacta, asegurando la auditoría de datos entre el equipo en terreno.



🚀 Próximos Pasos de Desarrollo
Configuración de la solución e infraestructura del proyecto .NET MAUI Blazor Hybrid.

Modelado de las clases de entidad y contexto de base de datos con EF Core (SQLite).

Diseño de la interfaz de usuario Blazor responsiva (Ficha Clínica).

Desarrollo de la Web API en .NET 9 para despliegue en Linux.