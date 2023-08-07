#!/bin/sh

allowedVersions=("major" "minor" "patch")
if  ! printf '%s\0' "${allowedVersions[@]}" | grep -Fxqz -- $1; then
    echo "Versions can only be 'major', 'minor' or 'patch'"
    exit
fi


echo "Updating $1"
cd src/WebUI/
npm version $1
cd ../..
VERSION=`find . -type f -name "package.json" | grep -v "node_module" | xargs cat | jq -r '.version'`
find . -type f -name "*.csproj" | xargs -L 1 dotnet setversion $VERSION 