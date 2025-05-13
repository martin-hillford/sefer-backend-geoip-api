#!/bin/bash
dotnet restore
dotnet build --no-restore 
dotnet publish Sefer.Backend.GeoIP.Api.csproj --output ./build