Integrantes del proyecto:

Jimena Jiménez Alfaro, Kenneth Navarro Pérez, Hersal Alfaro Cisneros y Carlos Hidalgo Leitón

Enlace al repositorio de git.

https://github.com/CarlosHidalgoLeiton/ProyectoPAWG1

Especificaciones básicas del proyecto:

a. Arquitectura del proyecto:

PAWG1.Api --> Proyecto que expone los datos y la lógica de negocio para que el mvc puede consumir los endpoints.

PAWG1.Architecture --> Proyecto que contiene funcionalidades que son comunes que son reutilizables.

PAWG1.Data --> Proyecto que se encarga de las iteraciones con la base de datos, en el se pueden encontrar los diferentes repositorios que manejan las operaciones a la base de datos.

PAWG1.Models --> Proyecto que tiene las definiciones de los modelos de datos que son las entidades de la base.

PAWG1.Mvc --> Proyecto que se encarga de la interfaz de usuario y de consumir las Apis para enviar y recibir los datos.

PAWG1.Service --> Proyecto que se encarga de la lógica del negocio llamando los diferentes repositorios.

PAWG1.Validator --> Proyecto que se encarga de las validaciones de los datos mas que todo del usuario.

b. Librerías o paquetes nuggets utilizados:

1. Microsoft.EntityFrameworkCore: ORM para interactuar con bases de datos en .NET.
2. Microsoft.EntityFrameworkCore.Abstractions: Interfaces y clases abstractas de EF Core.
3. Microsoft.EntityFrameworkCore.Design: Herramientas para migraciones y scaffolding en EF Core.
4. Microsoft.EntityFrameworkCore.Relational: Funcionalidades para bases de datos relacionales en EF Core.
5. Microsoft.EntityFrameworkCore.SqlServer: Proveedor EF Core para bases de datos SQL Server.
6. Microsoft.EntityFrameworkCore.Tools: Herramientas CLI para trabajar con EF Core.
7. Microsoft.VisualStudio.Web.CodeGeneration.Design: Soporte para generación de código en ASP.NET Core.
8. System.Security.Claims: Manejo de identidad y reclamaciones para autenticación y autorización.
9. Swashbuckle.AspNetCore: Generación de documentación Swagger para APIs ASP.NET Core.

c. Principios de SOLID y patrones de diseño utilizado

1. SOLID

Se aplico single responsability principle ya que las clases en el proyecto tienen responsabilidad unica.

Se aplico open/closed principle ya que las clases están abiertas a modificaciones.

Se aplico interface segragation principle ya que las interfaces creadas son pequeñas y específicas.

Se aplico dependency inversion principle ya que las clases que contienen como la lógica principal no dependen directamente de las clases que interactúan con la base de datos.

2. Patrones de diseño

Se uso el patron de diseño abstract factory para el manejo flexible y extensible de los repositorios.

























