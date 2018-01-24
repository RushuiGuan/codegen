cls

get-location | set-CompositeLocation;
get-location | set-ScenarioLocation;


New-DatabaseServer -DataSource localhost -InitialCatalog albatross -IntegratedSecurity | New-DatabaseTable -Name Contact -Schema crm  | Set-DatabaseTable .\contact.json -Force 
$table = Get-DatabaseTable .\contact.json;
$option = New-SqlQueryOption
Write-Code -Name table_update -Source $table -Option $option
$option.Variables;

<#

$comp = New-Composite -name UpdateWithIdentity -generator table_update,table_where_byid -target sql -st Albatross.CodeGen.Database.Table;
Set-Composite $comp .\UpdateWithIdentity.composite -force


#>