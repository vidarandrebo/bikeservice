clean:
	rm -rf src/Clietn/node_modules
	rm -rf src/Client/dist
	rm -rf src/Server/wwwroot/*
	rm -rf src/Server/Data
	find ./src/Infrastructure -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/Domain -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/Application -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/Server -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +

migration:
	dotnet ef migrations add $(name) --project src/Infrastructure --startup-project src/Server