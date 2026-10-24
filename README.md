# cgallegosTallerS3

Aplicación .NET MAUI para registrar contactos.

- **Vista 1 – `Views/ContactoPage`**: formulario con tipo de identificación (Picker: CI de 10 números, RUC de 13 números, Pasaporte libre), nombres, apellidos, fecha, correo y salario. Usa **estilos implícitos** (sin `x:Key`).
- **Vista 2 – `Views/ResumenPage`**: muestra los datos en Labels junto con el aporte al IESS (9.45 % del salario) y un botón para exportarlos a un archivo `.txt`. Usa **estilos explícitos** (con `x:Key`).
