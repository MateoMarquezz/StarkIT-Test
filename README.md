# Proyecto: API de Gestión de Usuarios || StarkIT-Test

Prueba técnica para la empresa StarkIT

Este proyecto es una API RESTful desarrollada en ASP.NET Core que permite gestionar y consultar una lista de usuarios. La API ofrece funcionalidades para obtener todos los usuarios y filtrarlos por género y por la inicial del nombre. Implementa inyección de dependencias, uso de interfaces, un sistema de registro personalizado en formato JSON y pruebas.

## Características

- **Obtención de Usuarios**: Consulta todos los usuarios disponibles.
- **Filtrado de Usuarios**: Filtra usuarios por género o por la inicial del nombre.
- **Registro en JSON**: Almacena los registros de actividad y errores en un archivo JSON.
- **Swagger**: Funcionamiento web de la API para probar los endpoints.
- **CORS**: Configuración para permitir el acceso desde cualquier origen (ya que es solo para tests).

## Tecnologías Utilizadas

- **C# .NET 6.0**
- **Moq** (para simulaciones en pruebas)
- **xUnit** (framework de pruebas unitarias)
- **Swagger** (documentación de la API)
- **JSON** (para almacenamiento de datos y registro de eventos)

## Arquitectura del Proyecto

El proyecto sigue una arquitectura en capas que separa las responsabilidades y facilita el mantenimiento:

- **Modelos**: Definen las estructuras de datos utilizadas.
  - **User**: Modelo que representa un usuario con propiedades `name` y `gender`.
  
- **Acceso a Datos (DataAccess)**: Maneja la interacción con la fuente de datos.
  - **UserRepository**: Lee los datos de usuarios desde un archivo JSON.
  
- **Servicios**: Contienen la lógica de negocio.
  - **IUserServices**: Interfaz que define los métodos del servicio de usuarios.
  - **UserServices**: Implementación de `IUserServices` que aplica la lógica de negocio y filtros.
  
- **Controladores**: Manejan las solicitudes HTTP y responden a los clientes.
  - **UserController**: Controlador que expone los endpoints de la API.
  
- **Registro de Eventos (Logging)**: Sistema de logging personalizado.
  - **JsonFileLogger**: Implementa `ILogger` para registrar eventos en formato JSON.
  - **JsonFileLoggerProvider**: Proveedor que crea instancias de `JsonFileLogger`.
  
- **Pruebas Unitarias**: Aseguran el correcto funcionamiento de los componentes.
  - **UserControllerTests**: Contiene pruebas unitarias para `UserController`.

---

**Consigna a seguir:** 
The client need an API to get names.

# Endpoints

## Backend
| 		                      | URL                                                                |
| --------------------------- | ------------------------------------------------------------------ |
| GET						  | /api/names               										   |

# Requirements
The client wants to filter the list of name by gender (M : Male, F :FEMALE) and Name, the name filter should be a "start with". 
If al filters are null os not setted the endpoint should return all names. 

- Create an API in NET
- Return a Json object
- Use differents layers  
- Logs
- Unit Tests
- Swagger or other documentation



# Tips
Don't be shy, try to show us all your knowledge, coding philosophy and best practices that are necessary to complete the challenge. 
