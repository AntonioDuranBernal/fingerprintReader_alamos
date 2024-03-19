# Api Lector de Huellas Alamos

Este proyecto es una API diseñada para manipular datos de huellas, tanto para el registro como para la autenticación.

## Instalación

1. Instalar el SDK proporcionado.
2. Abrir el proyecto con Visual Studio.
3. Modificar las credenciales de la base de datos local en el archivo `appseting.json`, en la sección `DefaultConnection`.
4. Compilar el proyecto.
5. Ejecutar `alams-Backend.exe` desde la carpeta `bin/Debug/net6.0`.

## Rutas

### 1. Endpoint `/FingerprintReader/find`

**Descripción del Endpoint:**

- Método: `POST`
- Parámetro: `List<string>[]`

Este endpoint compara la huella proporcionada con el campo `registroHuellaDigital` y busca el usuario correspondiente, devolviendo los resultados.

### 2. Endpoint `/FingerprintReader/register`

**Descripción del Endpoint:**

- Método: `POST`
Parámetro: `List<string>[]`

Este endpoint recibe como parámetro obligatorio cuatro huellas y devuelve un documento XML como resultado del registro.
