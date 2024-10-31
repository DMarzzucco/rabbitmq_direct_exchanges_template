# Utilizar la imagen base de .NET SDK para construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Establecer el directorio de trabajo en el contenedor
WORKDIR /app

# Copiar el archivo csproj y restaurar las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar el resto del código y construir el proyecto
COPY . ./
RUN dotnet build -c Release -o /app/build

# Publicar el proyecto
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Crear una imagen final para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyApi.dll"]
