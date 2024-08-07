# BikeService (work in progress)

Web app for managing your bike's service history and mileage.

## Frontend

Frontend written in Vue3.

## Backend

Backend written in ASP.Net Core

Before local developement of backend, make sure to set jwt secret in `src/Server` project. 

``` dotnet user-secrets set "Jwt:Secret" "<some32charsecret>"```