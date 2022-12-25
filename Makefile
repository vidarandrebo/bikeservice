build:
	dotnet build -o bin

clean:
	dotnet clean

restore:
	dotnet restore

watch:
	dotnet watch --project src/WebAPI/WebAPI.csproj

start:
	dotnet run --project src/WebAPI/WebAPI.csproj

migrations:
	dotnet ef migrations add init --project src/Infrastructure --startup-project src/WebAPI --output-dir src/Infrastructure/Migrations
