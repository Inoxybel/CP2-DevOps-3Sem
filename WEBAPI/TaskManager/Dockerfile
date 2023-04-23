FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine3.17 AS base
RUN adduser -S usuariocomum -s /bin/bash
WORKDIR /app
EXPOSE 80
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.17 AS build
ENV MongoDB "mongodb://mongodb:27017/TarefasDB"
WORKDIR /src
COPY ["TaskManager.csproj", "."]
RUN dotnet restore "./TaskManager.csproj"
COPY . .

WORKDIR "/src/."
RUN dotnet build "TaskManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER usuariocomum
ENTRYPOINT ["dotnet", "TaskManager.dll"]
