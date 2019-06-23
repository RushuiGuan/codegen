Import-Module app-dev
clear-host;

$app_root = (get-item $PSScriptRoot).Parent;

$src = get-path $app_root, src, Albatross.CodeGen.PowerShell;
$dst = get-path $app_root, dist, modules, codegen
Invoke-DotnetPublish -csproj $src -out $dst -config debug -framework netcoreapp2.2

Copy-Item -Path $PSScriptRoot\codegen.psd1 -Destination $dst\codegen.psd1 -Force;
Copy-Item -Path $PSScriptRoot\codegen.ps1 -Destination $dst\codegen.ps1 -Force;

$src = get-path $app_root, src, Albatross.CodeGen.ClassLoader;
$dst = get-path $app_root, dist, modules, codegen, classloader
Invoke-DotnetPublish -csproj $src -out $dst -config release -runtime win-x64 -selfContained
