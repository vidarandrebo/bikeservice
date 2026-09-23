FROM node:26.10.0-alpine3.24 AS node-build-env

WORKDIR /data
ENV CI="TRUE"

COPY ./src/Client/ /data/

RUN apk add pnpm --no-cache
RUN pnpm install --frozen-lockfile
RUN pnpm build

FROM mcr.microsoft.com/dotnet/sdk:10.0.401-alpine3.24 AS dotnet-build-env
WORKDIR /data

# Copy everything
COPY . .
COPY --from=node-build-env /data/dist/ /data/src/Server/wwwroot/
# Build and publish a release

RUN dotnet publish /data/src/Server -c Release -o bin

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0.12-alpine3.24
WORKDIR /data
COPY --from=dotnet-build-env /data/bin/ .
ENTRYPOINT ["dotnet","BikeService.Server.dll"]