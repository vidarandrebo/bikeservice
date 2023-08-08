clean:
	rm -rf src/WebUI/node_modules
	rm -rf src/WebUI/dist
	rm -rf src/WebAPI/wwwroot/*
	rm -rf src/WebAPI/Data
	find ./src/Infrastructure -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/Domain -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/Application -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/WebAPI -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +

migration:
	cp docker.env src/WebAPI/.env
	dotnet ef migrations add $(name)Production --project src/Infrastructure --startup-project src/WebAPI --output-dir Migrations --context NpgsqlContext -v -- --environment Production
	dotnet ef migrations add $(name)Dev --project src/Infrastructure --startup-project src/WebAPI --output-dir Migrations --context SqliteContext
	rm src/WebAPI/.env