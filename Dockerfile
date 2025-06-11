# Imagen base
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["PagosCESvsCDC.csproj", "./"]
RUN dotnet restore "./PagosCESvsCDC.csproj"

COPY . .
WORKDIR "/src"
RUN dotnet publish "PagosCESvsCDC.csproj" -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PagosCESvsCDC.dll"]
