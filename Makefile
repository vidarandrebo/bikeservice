build:
	dotnet build src/WebAPI -o bin/debug

build-release:
	dotnet publish src/WebAPI -c Release -o bin/release

clean:
	rm -rf bin
	find ./src/Infrastructure -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/Domain -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/Application -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/WebAPI -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +

restore:
	dotnet restore

watch:
	dotnet watch --project src/WebAPI/WebAPI.csproj

start:
	dotnet run --project src/WebAPI/WebAPI.csproj

migrations:
	cp .env src/WebAPI/
	dotnet ef migrations add Init --project src/Infrastructure --startup-project src/WebAPI --output-dir Migrations
	dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI
	rm src/WebAPI/.env

rebuild-db:
	cp .env src/WebAPI/
	dotnet ef database drop -f -v --project src/Infrastructure/ --startup-project src/WebAPI/
	find ./src/Infrastructure -type d \( -name "Migrations" \) -exec rm -rf {} +
	dotnet ef migrations add Init --project src/Infrastructure --startup-project src/WebAPI --output-dir Migrations
	dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI
	rm src/WebAPI/.env
	make clean