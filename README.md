# star-wars-api-net

### Produccion

El proyecto se encuentra en la url: https://api-demo-hyftf3dzftf9esdu.canadacentral-01.azurewebsites.net/index.html

Consideraciones:

-   Por defecto la creación de usuarios se hacen con el rol User.
-   El usuario Admin tiene acceso a todos los endpoint.
-   El usuario User solo tiene acceso a listar todas las peliculas y por id.

Usuario admin:

-   username: admin
-   password: Admin123!

### Correr localmente

Se utilizó user secrets, por lo que debe correrse una instancia local de postgres, configurar la conexión en la ruta de user secrets de acuerdo al sistema operativo, u opcionalmente agregarlo en appsettings.Development.json (si se corre como entorno development, configurandolo desde MoviesProject.WebApi/Properties/launchSettings.json)

```json
{
    "ConnectionStrings": {
        "MainConnection": "your_db_connection"
    }
}
```
