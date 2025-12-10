Get-ChildItem $PSScriptRoot/*.py | ForEach-Object {
    $line = Get-Content $_.FullName -TotalCount 1;
    if ($line -eq '# @generated') {
        Write-Host "Removing generated file $($_.FullName)"
        Remove-Item $_.FullName
    }
}

Get-ChildItem $PSScriptRoot/codegen/models | Remove-Item;


$root = git rev-parse --show-toplevel
$location = Get-Location

Set-Location $root/Albatross.CodeGen.CommandLine
dotnet run --no-launch-profile -- schema python --file $PSScriptRoot/codegen/codegen-settings.schema.json
dotnet run --no-launch-profile -- model dto -p $root/Test.Dto/Test.Dto.csproj -s $PSScriptRoot/codegen/codegen-settings.json -o $PSScriptRoot/codegen/models/
dotnet run --no-launch-profile -- model controller -p $root/Test.WebApi/Test.WebApi.csproj -s $PSScriptRoot/codegen/codegen-settings.json -o $PSScriptRoot/codegen/models/
dotnet run --no-launch-profile -- py dto  -p $root/Test.Dto/Test.Dto.csproj -s $PSScriptRoot/codegen/codegen-settings.json -o $PSScriptRoot/ --show-stack
dotnet run --no-launch-profile -- py web-client -p $root/Test.WebApi/Test.WebApi.csproj -s $PSScriptRoot/codegen/codegen-settings.json -o $PSScriptRoot/
Set-Location $location