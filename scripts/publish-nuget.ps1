$ErrorActionPreference = "stop";
Import-Module app-dev;
$app_root = Get-Item $PSScriptRoot\..;

$array = @(
	"$app_root\src\Albatross.CodeGen",
	"$app_root\src\Albatross.CodeGen.Autofac"
);

$array | ForEach-Object {
	$out = Get-Path Get-NugetLocal, (get-item $_).Name;
	Invoke-DotnetPack -csproj $_  -config debug  -out $out;
}