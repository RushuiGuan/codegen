cls
& $PSScriptRoot\setup.ps1
Register-Assembly;
$root = get-item $PSScriptRoot\..\..\..;
$options = New-Object Albatross.CodeGen.Generation.CRUDProjectOptions 
$options.Schema = "ac";
$options.Name = "Principal";
$options.Database = New-Database -server localhost -database ac -i;
$options.InterfaceLocation = "$root\src\Albatross.AccessControl.Core";
$options.ClassLocation = "$root\src\Albatross.AccessControl.Core";
$options.StoredProcedureLocation = "$root\src\ac-db\Principal";
$options.DataLayerApiLocation = "$root\src\Albatross.AccessControl.DataLayer";

$table = Get-DatabaseTable -database $option.Database -schema $option.Schema -name $option.Name;
# CSharp Class object
Invoke-Composite -Source $procedure -Option $option -b csharp.namespace, csharp.procedure.dapper  -Output "$location\$name.cs" -Force;

# Create
$name = "Create$($table.Name)";
$schema = "ac";
$permission = new-databasepermission -state grant -permission execute -principal app_svc;
$procedure = New-StoredProcedure -name $name -schema $schema -filter ByIdentityColumn -database $table.Database -permission $permission -gs $table -branch sql.insert, newline, sql.select.identity
$procedure = publish-StoredProcedure $procedure;

$location = get-item $root\ac\src\Albatross.AccessControl.DataLayer;
$option = New-CSharpClassOption -Name $name  -namespace Albatross.AccessControl.DataLayer -imports System,Dapper, System.Data;
