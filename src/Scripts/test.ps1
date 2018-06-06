cls
& $PSScriptRoot\setup.ps1
Register-Assembly;

function Create-CRUD([Albatross.Database.Table]$table, [string]$rootNamespace, [string]$basePath, [string]$classPath, [string]$interfacePath, [string]$implementationPath, [string]$storedProcedurePath, [string]$dataLayerPath ){
    [string]$name = $table.Name;
    [Albatross.CodeGen.Genenration.ClassOption]$classOption = New-CSharpClassOption -Name $name -Namespace "$rootNamespace.Core" -Imports System -overrides 
    Invoke-Composite -Source $table -Option $classOption -b csharp.namespace, csharp.table.class -Output "$basePath\$classPath\$name.cs" -Force;

}

$root = get-item $PSScriptRoot\..\..\..;

[Albatross.Database.Table]$table = Get-DatabaseTable -database $options.Database -criteria "$($options.Schema).$($options.Name)";
# C# Class
