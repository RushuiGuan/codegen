cls
& $PSScriptRoot\setup.ps1
Register-Assembly;
$root = get-item $PSScriptRoot\..\..\..;

$location = get-item $root\ac\src\ac-db\principal;
$table = Get-DatabaseTable -DbName ac -Criteria ac.Principal;
$name = "Create$($table.Name)";
$schema = "ac";
$permission = new-databasepermission -state grant -permission execute -principal app_svc;
$procedure = New-StoredProcedure -name $name -schema $schema -filter ByIdentityColumn -database $table.Database -permission $permission -gs $table -branch sql.insert, newline, sql.select.identity
publish-StoredProcedure $procedure;

$location = get-item $root\ac\src\Albatross.AccessControl.DataLayer;
$option = New-CSharpClassOption -Name $name  -namespace Albatross.AccessControl.DataLayer -imports System,Dapper, System.Data;
$procedure = get-storedProcedure -p $procedure;
Invoke-Composite -Source $procedure -Option $option -b csharp.namespace, csharp.procedure.dapper  -Output "$location\$name.cs" -Force;
