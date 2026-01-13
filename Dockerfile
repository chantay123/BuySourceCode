# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY WebBuySource/WebBuySource.csproj WebBuySource/
RUN dotnet restore WebBuySource/WebBuySource.csproj

COPY . .

# Publish ứng dụng
RUN dotnet publish WebBuySource/WebBuySource.csproj \
    -c Release \
    -o /app/publish \
    --no-restore


# =========================
# RUNTIME STAGE
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# ENV cho container
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true

EXPOSE 8080

ENTRYPOINT ["dotnet", "WebBuySource.dll"]

