FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

ENV ASPNETCORE_URLS=http://+:5000;http://+:5001
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ../SystemCcharpzinho.API/SystemCcharpzinho.API.csproj SystemCcharpzinho.API/
COPY ../SystemCcharpzinho.Request/SystemCcharpzinho.Request.csproj SystemCcharpzinho.Request/
COPY ../SystemCcharpzinho.Core/SystemCcharpzinho.Core.csproj SystemCcharpzinho.Core/
COPY ../SystemCcharpzinho.Infrastructure/SystemCcharpzinho.Infrastructure.csproj SystemCcharpzinho.Infrastructure/
RUN dotnet restore "SystemCcharpzinho.API/SystemCcharpzinho.API.csproj"
COPY . . 
WORKDIR "/src/SystemCcharpzinho.API"
RUN dotnet build "SystemCcharpzinho.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SystemCcharpzinho.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . 
ENTRYPOINT ["dotnet", "SystemCcharpzinho.API.dll"]
