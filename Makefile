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
	cp local.env src/WebAPI/.env
	dotnet ef migrations add $(name) --project src/Infrastructure --startup-project src/WebAPI --output-dir Migrations
	rm src/WebAPI/.env
