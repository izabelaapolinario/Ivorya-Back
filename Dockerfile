# Etapa 1: Usando a imagem base do .NET 9.0
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa 2: Usando uma imagem do SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ivorya-back.csproj", "./"]
RUN dotnet restore "ivorya-back.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ivorya-back.csproj" -c Release -o /app/build

# Etapa 3: Publicando a aplicação
FROM build AS publish
RUN dotnet publish "ivorya-back.csproj" -c Release -o /app/publish

# Etapa 4: Criando a imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ivorya-back.dll"]
