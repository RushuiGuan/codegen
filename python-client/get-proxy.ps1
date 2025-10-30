$root = git rev-parse --show-toplevel
$location = Get-Location

Set-Location $root/Albatross.CodeGen.CommandLine
dotnet run --no-launch-profile -- settings-schema --file ../python-client/codegen/codegen-settings.schema.json
dotnet run --no-launch-profile -- py dto  -p ../Test.Dto/Test.Dto.csproj -s ../python-client/codegen/codegen-settings.json -o ../python-client/
dotnet run --no-launch-profile -- model dto -p ../Test.Dto/Test.Dto.csproj -s ../python-client/codegen/codegen-settings.json -o ../python-client/codegen/models/
dotnet run --no-launch-profile -- model controller -p ../Test.WebApi/Test.WebApi.csproj -s ../python-client/codegen/codegen-settings.json -o ../python-client/codegen/models/
dotnet run --no-launch-profile -- py web-client -p ../Test.WebApi/Test.WebApi.csproj -s ../python-client/codegen/codegen-settings.json -o ../python-client/
Set-Location $location