clean:
	rm -rf src/WebUI/node_modules
	rm -rf src/WebUI/dist
	find ./src/Infrastructure -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/Domain -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/Application -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +
	find ./src/WebAPI -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +

migration:
	cp local.env src/WebAPI/.env
	dotnet ef migrations add $(name) --project src/Infrastructure --startup-project src/WebAPI --output-dir Migrations
	rm src/WebAPI/.env

update-db:
	cp local.env src/WebAPI/.env
	dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI
	rm src/WebAPI/.env

delete-db:
	cp local.env src/WebAPI/.env
	dotnet ef database drop -f -v --project src/Infrastructure/ --startup-project src/WebAPI/
	rm src/WebAPI/.env
