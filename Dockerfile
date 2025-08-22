FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copia toda a solution
COPY . .

# Restaura todos os projetos
RUN dotnet restore "src/BigECommerce.Api/BigECommerce.Api.csproj"

# Build da API
WORKDIR /src/src/BigECommerce.Api
RUN dotnet build "BigECommerce.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BigECommerce.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BigECommerce.Api.dll"]
