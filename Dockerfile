# Etapa 1: Escolhendo a imagem base para a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80  # A porta exposta para a aplicação

# Etapa 2: Usando uma imagem do SDK para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copia o arquivo .csproj e restaura as dependências
COPY ["ivorya-back.csproj", "./"]
RUN dotnet restore "ivorya-back.csproj"

# Copia os arquivos restantes
COPY . .

# Compila a aplicação
WORKDIR "/src/."
RUN dotnet build "ivorya-back.csproj" -c Release -o /app/build

# Etapa 3: Publicando a aplicação
FROM build AS publish
RUN dotnet publish "ivorya-back.csproj" -c Release -o /app/publish

# Etapa 4: Imagem final para produção
FROM base AS final
WORKDIR /app

# Copia os arquivos publicados da etapa de build
COPY --from=publish /app/publish .

# A aplicação escutará na porta definida pela plataforma
ENTRYPOINT ["dotnet", "ivorya-back.dll"]
