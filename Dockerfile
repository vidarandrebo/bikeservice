FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY . .
# Build and publish a release
RUN dotnet publish /App/src/WebAPI -c Release -o bin

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/bin .
COPY --from=build-env /App/bin/appsettings.json .
RUN true
COPY .env .
RUN true
ENTRYPOINT ["dotnet","WebAPI.dll"]