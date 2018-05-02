& $PSScriptRoot\Setup.ps1;

$option = New-CSharpClassOption -Imports System,Dapper -Namespace Albatross.AccessControl.DataLayer
$procedures = ,(Get-StoredProcedure -DbName albatross -Criteria sec.%  | Sort-Object -Property Name | New-Leaf -name "procedure command definition") | new-branch
$namespace = New-Leaf -Name "csharp namespace"
New-Branch -Nodes $namespace, $procedures | Invoke-Composite -Source (New-Object System.Object) -Option $option