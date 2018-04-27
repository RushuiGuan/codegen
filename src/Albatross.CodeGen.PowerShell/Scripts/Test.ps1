cls

Register-Assembly

$server = New-DatabaseServer -DataSource localhost -InitialCatalog albatross -IntegratedSecurity;
$table = New-DatabaseTable -Name Contact -Schema crm  -Database $server;
$option = New-SqlQueryOption -Filter ByPrimaryKey


Invoke-CodeGenerator -Name table_update -Source $table -Option $option -Output C:\Temp\test.sql -Force

Invoke-Composite -Branch (New-Branch table_update,newline, table_where) -Source $table -Option $option -Output c:\temp\output.sql -Force;

<#

$comp = New-Composite -name UpdateWithIdentity -generator table_update,table_where_byid -target sql -st Albatross.CodeGen.Database.Table;
Set-Composite $comp .\UpdateWithIdentity.composite -force


#>
