# Usar a imagem base do SDK do .NET para compilar a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiar os arquivos de solução e projetos para o diretório de trabalho no container
COPY ./OrdersService.Api/*.csproj ./OrdersService.Api/
COPY ./OrdersService.Application/*.csproj ./OrdersService.Application/
COPY ./OrdersService.Infrastructure/*.csproj ./OrdersService.Infrastructure/
COPY ./OrdersService.Domain/*.csproj ./OrdersService.Domain/

# Restaurar as dependências dos projetos
RUN dotnet restore "OrdersService.Api/OrdersService.Api.csproj"

# Copiar todos os arquivos restantes e construir a aplicação
COPY ../. ./
RUN dotnet publish OrdersService.Api -c Release -o out

# Usar a imagem base do ASP.NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar os arquivos publicados da fase de build
COPY --from=build-env /app/out .

# Definir a porta que a aplicação irá escutar
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "OrdersService.Api.dll"]