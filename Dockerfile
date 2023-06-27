FROM node:20-alpine as node-build-env
#ENV NODE_ENV=production

WORKDIR /data

COPY ./src/WebUI/ /data/

RUN npm ci

RUN npm run buildRelease

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS dotnet-build-env
WORKDIR /data

# Copy everything
COPY . .
COPY --from=node-build-env /data/dist/ /data/src/WebAPI/wwwroot/
# Build and publish a release

RUN dotnet publish /data/src/WebAPI -c Release -o bin

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /data
COPY --from=dotnet-build-env /data/bin/ .
#COPY --from=dotnet-build-env /data/bin/appsettings.json .
RUN true
COPY docker.env .env
RUN true
EXPOSE 8080
ENTRYPOINT ["dotnet","WebAPI.dll"]