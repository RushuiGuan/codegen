cls;
$project = Get-Item $PSScriptRoot\..\..
$src = Get-Item $PSScriptRoot\..
Import-Module $src\Albatross.CodeGen.PowerShell\bin\debug\net462\Albatross.CodeGen.PowerShell.dll;
register-assembly;

