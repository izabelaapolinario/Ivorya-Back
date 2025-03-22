# Etapa 1: Escolhendo a imagem base para a aplicação
# A imagem base é onde a aplicação será executada, sem o SDK de desenvolvimento
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80  # Porta de exposição para HTTP
EXPOSE 443 # Porta de exposição para HTTPS

# Etapa 2: Usando uma imagem do SDK para compilar a aplicação
# Aqui utilizamos a imagem do SDK, que contém as ferramentas para compilar o código
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

# Copia o arquivo .csproj e restaura as dependências (comando dotnet restore)
COPY ["ivorya-back.csproj", "./"]
RUN dotnet restore "ivorya-back.csproj"

# Copia todos os outros arquivos do projeto para o contêiner
COPY . .

# Define o diretório de trabalho para o build e compila o projeto
WORKDIR "/src/."
RUN dotnet build "ivorya-back.csproj" -c Release -o /app/build

# Etapa 3: Publicando a aplicação
# O comando "dotnet publish" prepara a aplicação para produção, copiando o código e as dependências necessárias
FROM build AS publish
RUN dotnet publish "ivorya-back.csproj" -c Release -o /app/publish

# Etapa 4: Criando a imagem final
# Aqui usamos a imagem base para rodar a aplicação
FROM base AS final

WORKDIR /app

# Copia os arquivos publicados da etapa de build para o contêiner final
COPY --from=publish /app/publish .

# Define o comando que será executado quando o contêiner for iniciado
ENTRYPOINT ["dotnet", "ivorya-back.dll"]
