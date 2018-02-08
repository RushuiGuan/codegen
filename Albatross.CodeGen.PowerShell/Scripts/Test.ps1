cls

$server = New-DatabaseServer -DataSource localhost -InitialCatalog albatross -IntegratedSecurity;
$table = New-DatabaseTable -Name Contact -Schema crm  -Server $server;
$option = New-SqlQueryOption

Invoke-CodeGenerator -Name table_update -Source $table -Option $option

New-Composite -SourceType Albatross.CodeGen.Database.DatabaseObject 

<#

$comp = New-Composite -name UpdateWithIdentity -generator table_update,table_where_byid -target sql -st Albatross.CodeGen.Database.Table;
Set-Composite $comp .\UpdateWithIdentity.composite -force


#>