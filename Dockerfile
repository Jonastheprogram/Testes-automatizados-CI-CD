# Estágio 1: Build 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Restaura as dependências da solução inteira 
COPY *.sln .
COPY Api.Esg.Fiap/*.csproj ./Api.Esg.Fiap/
RUN dotnet restore

# Copia todo o código
COPY . .



# Publica a aplicação 
WORKDIR /source/Api.Esg.Fiap
RUN dotnet publish -c Release -o /app/out --no-restore

# Estágio 2: Final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app


EXPOSE 8080

# Copia apenas o resultado final da publicação
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Api.Esg.Fiap.dll"]