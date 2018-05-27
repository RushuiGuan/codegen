cls;
$project = Get-Item $PSScriptRoot\..\..
$src = Get-Item $PSScriptRoot\..
Import-Module $src\bin\Albatross.CodeGen.PowerShell.dll;
register-assembly;