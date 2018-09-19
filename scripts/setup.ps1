Import-Module $PSScriptRoot\..\src\Albatross.CodeGen.PowerShell\bin\Debug\net462\Albatross.CodeGen.PowerShell.dll
Register-Assembly;
$db = New-DatabaseServer -DataSource localhost -InitialCatalog Albatross -IntegratedSecurity
$table = New-DatabaseTable -Name Company -Schema access -Database $db;
Invoke-CodeGenerator -name table_insert -Source $table;