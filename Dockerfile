FROM node:24-slim AS node-build-env

WORKDIR /data
ENV CI="TRUE"

COPY ./src/Client/ /data/

RUN corepack enable
RUN pnpm install --frozen-lockfile
RUN pnpm build

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS dotnet-build-env
WORKDIR /data

# Copy everything
COPY . .
COPY --from=node-build-env /data/dist/ /data/src/Server/wwwroot/
# Build and publish a release

RUN dotnet publish /data/src/Server -c Release -o bin

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /data
COPY --from=dotnet-build-env /data/bin/ .
#COPY --from=dotnet-build-env /data/bin/appsettings.json .
RUN true
COPY docker.env .env
RUN true
ENTRYPOINT ["dotnet","BikeService.Server.dll"]