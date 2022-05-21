dotnet ef database drop -f -v
rm -rf Data/Migrations
dotnet ef migrations add init -o Data/Migrations
dotnet ef database update
