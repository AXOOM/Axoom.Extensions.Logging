Param ([string]$Version = "0.1-debug")
$ErrorActionPreference = "Stop"
Push-Location $(Split-Path -Path $MyInvocation.MyCommand.Definition -Parent)

dotnet clean
dotnet msbuild /t:Restore /t:Build /p:Configuration=Release /p:Version=$Version
dotnet test --configuration Release --no-build "Axoom.Extensions.Logging.UnitTests/Axoom.Extensions.Logging.UnitTests.csproj"

Pop-Location
