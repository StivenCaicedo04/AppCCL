# AppCCL

Aplicación Web API en .NET 9 para gestionar productos e inventario con autenticación JWT.

## Resumen

`AppCCL` es una API REST que provee:
- Gestión de productos (`ProductosController`)
- Registro de movimientos de inventario (`MovimientosController`)
- Autenticación básica basada en JWT (`AuthController`)
- Persistencia en PostgreSQL con Entity Framework Core
- Documentación Swagger integrada

## Arquitectura

El proyecto sigue una arquitectura en capas con los siguientes componentes principales:

- `Controllers`: Exponen los endpoints HTTP.
- `Services`: Contienen la lógica de negocio.
- `Repository`: Acceso a datos con Entity Framework.
- `Data`: Contexto de EF Core (`AppDbContext`).
- `Models`: Entidades de base de datos.
- `DTOs`: Objetos para transporte de datos entre capa API y lógica de negocio.
- `Interfaces`: Contratos para servicios y repositorios.

## Funcionalidades principales

### Productos

EndPoints disponibles en `api/productos`:

- `GET api/productos` - Obtiene todos los productos (requiere JWT).
- `POST api/productos` - Crea un producto.
- `PUT api/productos/{id}` - Actualiza un producto.
- `DELETE api/productos/{id}` - Elimina un producto.

Los productos contienen:
- `Id`
- `Nombre`
- `Stock`
- `Fechacreacion` (solo en la entidad de base de datos)

### Movimientos de inventario

EndPoints disponibles en `api/movimientos`:

- `GET api/movimientos` - Obtiene todos los movimientos registrados (requiere JWT).
- `POST api/movimientos` - Registra un movimiento de inventario.

La lógica de movimiento admite:
- `ENTRADA` para aumentar stock
- `SALIDA` para disminuir stock

### Autenticación

EndPoint disponible en `api/auth/login`:

- `POST api/auth/login` - Genera token JWT.

Credenciales fijas para login:
- Email: `admin@test.com`
- Password: `1234`

El token JWT se genera con:
- `Issuer`: `AppCCLAuth`
- `Audience`: `FrontAppCCL`
- `ExpiresInMinutes`: `30`

## Configuración

Valores importantes en `appsettings.json`:

- `ConnectionStrings:DefaultConnection` - Cadena de conexión PostgreSQL.
- `Jwt:Key` - Clave secreta para firmar el token.
- `Jwt:Issuer` / `Jwt:Audience` - Valores de validación del token.
- `Jwt:ExpiresInMinutes` - Tiempo de vida del token.

### CORS

Se habilita la política `AngularPolicy` permitiendo solicitudes desde:
- `http://localhost:4200`

## Dependencias principales

- `Microsoft.AspNetCore.Authentication.JwtBearer` - Autenticación JWT.
- `Microsoft.EntityFrameworkCore` - ORM.
- `Npgsql.EntityFrameworkCore.PostgreSQL` - Proveedor PostgreSQL.
- `Swashbuckle.AspNetCore` - Swagger / OpenAPI.

## Base de datos

Tablas definidas en `AppDbContext`:

- `productos`
- `movimientosinventario`

Relación:
- `Producto` 1:N `MovimientosInventario`

## Instrucciones de ejecución

1. Asegúrate de tener PostgreSQL en `localhost:5432` y la base de datos `DataBaseCCL` creada.
2. Actualiza la cadena de conexión si es necesario.
3. Ejecuta el proyecto desde Visual Studio o con `dotnet run`.
4. Accede a Swagger en `https://localhost:{puerto}/swagger`.

## Notas importantes

- Todos los endpoints de productos y movimientos requieren un token JWT válido.
- El login usa credenciales hardcodeadas, por lo tanto no es adecuado para producción.
- El control de stock de salida verifica que haya suficiente inventario antes de restar.
- Los métodos de repositorio guardan cambios inmediatamente con `SaveChangesAsync()`.

## Mejoras sugeridas

- Añadir administración real de usuarios y contraseñas en base de datos.
- Implementar migraciones EF Core para gestionar esquema de base de datos.
- Ampliar la validación de DTOs y manejar errores con middleware global.
- Agregar endpoints para consultar un producto por ID.
- Crear pruebas unitarias y de integración.
