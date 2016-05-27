#!/bin/bash
export DOTNET_INSTALL_DIR="$PWD/.dotnetsdk"
export PATH="$DOTNET_INSTALL_DIR:$PATH"
cd src
dotnet restore
cd ../test
dotnet restore
cd ..
cd src/Pivotal.Extensions.Configuration.ConfigServer
dotnet build --framework netstandard1.3 --configuration Release
cd ../..
