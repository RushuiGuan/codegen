Get-ChildItem $PSScriptRoot/*.py | ForEach-Object {
    $line = Get-Content $_.FullName -TotalCount 1;
    if ($line -eq '# @generated') {
        Write-Host "Removing generated file $($_.FullName)"
        Remove-Item $_.FullName
    }
}



$root = git rev-parse --show-toplevel
$location = Get-Location

Set-Location $root/Albatross.CodeGen.CommandLine
dotnet run --no-launch-profile -- schema settings --file $PSScriptRoot/codegen-settings.schema.json
dotnet run --no-launch-profile -- py dto  -p ../Test.Dto/Test.Dto.csproj -s ../python-client/codegen/codegen-settings.json -o ../python-client/
dotnet run --no-launch-profile -- model dto -p ../Test.Dto/Test.Dto.csproj -s ../python-client/codegen/codegen-settings.json -o ../python-client/codegen/models/
dotnet run --no-launch-profile -- model controller -p ../Test.WebApi/Test.WebApi.csproj -s ../python-client/codegen/codegen-settings.json -o ../python-client/codegen/models/
dotnet run --no-launch-profile -- py web-client -p ../Test.WebApi/Test.WebApi.csproj -s ../python-client/codegen/codegen-settings.json -o ../python-client/
Set-Location $location