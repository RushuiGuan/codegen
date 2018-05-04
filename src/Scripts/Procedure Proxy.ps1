& $PSScriptRoot\Setup.ps1;

$option = New-CSharpClassOption -Imports System,Dapper -Namespace Albatross.AccessControl.DataLayer
$procedures = Get-StoredProcedure -DbName ac -Criteria ac.%  | Sort-Object -Property Name | New-Leaf -name "sql.procedure.dapper"
$namespace = New-Leaf -Name "csharp.namespace"
New-Branch -Nodes (@($namespace) + $procedures) | Invoke-Composite -Source (New-Object System.Object) -Option $option