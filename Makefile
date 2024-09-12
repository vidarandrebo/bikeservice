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
	dotnet ef migrations add $(name) --project src/Migrations.Postgres --startup-project src/Server -v -- --environment Production
	dotnet ef migrations add $(name) --project src/Migrations.Sqlite --startup-project src/Server


rm-migrations:
	rm -rf ./src/Migrations.Sqlite/Migrations/*
	rm -rf ./src/Migrations.Postgres/Migrations/*