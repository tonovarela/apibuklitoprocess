# apiBukLitoprocess

Este proyecto es una API en ASP.NET Core para recibir webhooks relacionados con colaboradores.

## Características
- Recibe eventos tipo `employee_update` mediante un endpoint webhook.
- Deserializa datos de empleados y procesa eventos.

## Uso


1. Instala .NET 8.0 si no lo tienes.
2. Ejecuta el proyecto:
   ```bash
   dotnet watch run
   ```
3. El endpoint principal es:
   - POST `/api/colaborador/webhook`
   - Recibe un JSON con la estructura:
     ```json
     {
       "data": {
         "event_type": "employee_update",
         "date": "2026-02-26T18:34:25-06:00",
         "tenant_url": "litoprocess.buk.mx",
         "employee_id": 3256,
         "employment_status": "activo"
       }
     }
     ```

## Estructura del proyecto
- Controllers/
- Models/
- DTOs/
- Services/
- Repositories/

## Recomendaciones
- Configura variables de entorno y archivos de configuración según tu entorno.
- Usa ngrok para exponer localmente el endpoint si necesitas pruebas externas.

## Licencia
Este proyecto es de uso educativo y puede ser modificado según tus necesidades.


ngrok http 80