cls

Import-Module $PSScriptRoot\..\src\Albatross.CodeGen.PowerShell\bin\Debug\net462\Albatross.CodeGen.PowerShell.dll
Register-Assembly;

get-codegenerator | ForEach-Object{$_.Name;}

Get-CodeGenerator table-to-class

New-DatabaseServer -DataSource . -InitialCatalog Albatross -IntegratedSecurity `
    | Get-storedprocedure delete*

