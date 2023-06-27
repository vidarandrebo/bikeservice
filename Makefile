build-debug: build-backend-debug build-frontend-debug

build-release: build-backend-release build-frontend-release

build-frontend-release: install-frontend
	npm run buildRelease --prefix src/WebUI

build-frontend-debug: install-frontend
	npm run build --prefix src/WebUI

build-backend-debug: restore
	dotnet build --no-restore src/WebAPI -o bin/debug

build-backend-release: restore
	dotnet publish src/WebAPI -c Release -o bin/release

clean:
	rm -rf src/WebUI/node_modules
	rm -rf src/WebUI/dist
	rm -rf bin
	find ./src/Infrastructure -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/Domain -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/Application -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/WebAPI -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +

install-frontend:
	npm --prefix src/WebUI ci


restore:
	dotnet restore

watch:
	dotnet watch --project src/WebAPI/WebAPI.csproj

start:
	dotnet run --project src/WebAPI/WebAPI.csproj

migrations:
	cp local.env src/WebAPI/.env
	dotnet ef migrations add Init --project src/Infrastructure --startup-project src/WebAPI --output-dir Migrations
	dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI
	rm src/WebAPI/.env

rebuild-db:
	cp local.env src/WebAPI/.env
	dotnet ef database drop -f -v --project src/Infrastructure/ --startup-project src/WebAPI/
	find ./src/Infrastructure -type d \( -name "Migrations" \) -exec rm -rf {} +
	dotnet ef migrations add Init --project src/Infrastructure --startup-project src/WebAPI --output-dir Migrations
	dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI
	rm src/WebAPI/.env
	make clean
