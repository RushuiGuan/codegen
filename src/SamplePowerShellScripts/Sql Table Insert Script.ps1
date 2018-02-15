cls;
set-location (Get-Item $PSScriptRoot).Parent.FullName;

Import-Module .\Albatross.CodeGen.PowerShell\bin\debug\net462\Albatross.CodeGen.PowerShell.dll;
register-assembly;

$db = New-DatabaseServer -DataSource localhost -InitialCatalog albatross -IntegratedSecurity;
$table = New-DatabaseTable -Server $db -Schema dbo -Name Contact;
$option = New-SqlQueryOption 


Invoke-CodeGenerator -Name table_insert -Source $table -Option $option;