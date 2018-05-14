cls
& $PSScriptRoot\setup.ps1
Register-Assembly;
$root = get-item $PSScriptRoot\..\..\..;

$options = New-Object Albatross.CodeGen.Generation.CRUDProjectOptions 
$options.Schema = "ac";
$options.Name = "Principal";
$options.Database = New-Database -server localhost -database ac -i;
$options.InterfacePath = "$root\ac\src\Albatross.AccessControl.Core";
$options.ImplementationPath = "$root\ac\src\Albatross.AccessControl";
$options.ClassPath = "$root\ac\src\Albatross.AccessControl.Core";
$options.StoredProcedurePath = "$root\ac\src\ac-db\Principal";
$options.DataLayerApiPath = "$root\ac\src\Albatross.AccessControl.DataLayer";

$options.ClassOption = new-CSharpClassOption -name $options.Name -namespace Albatross.AccessControl.Core -imports System -overrides @{ PrincipalType = "PrincipalType" };
$options.DatabasePermissions = @(new-databasepermission -state grant -permission execute -principal app_svc);

[Albatross.Database.Table]$table = Get-DatabaseTable -database $options.Database -criteria "$($options.Schema).$($options.Name)";
# C# Class
Invoke-Composite -Source $table -Option $options.ClassOption -b csharp.namespace, csharp.table.class -Output "$($options.ClassPath)\$($options.Name).cs" -Force;

# Create Stored Procedure
$procedure = New-StoredProcedure -name "Create$($options.Name)" -schema $options.Schema -filter ByIdentityColumn -database $options.Database -permission $options.DatabasePermissions -gs $table -branch sql.insert, newline, sql.select.identity
$procedure = publish-StoredProcedure $procedure;

# Create Stored Procedure Proxy
$classOption = new-CSharpClassOption -name "Create$($options.Name)" -namespace Albatross.AccessControl.DataLayer -imports System, Dapper, System.Data;
Invoke-Composite -Source $procedure -Option $classOption -b csharp.namespace, csharp.procedure.dapper -Output "$($options.DataLayerApiPath)\$($classOption.Name).cs" -Force;

# Create interfaces
$classOption = new-CSharpClassOption -name $options.Name -namespace Albatross.AccessControl.Core -imports System,System.Collections.Generic;
Invoke-CodeGenerator -Option $classOption -n csharp.crud.create.interface -Output "$($options.InterfacePath)\ICreate$($options.Name).cs" -Force;
Invoke-CodeGenerator -Option $classOption -n csharp.crud.update.interface -Output "$($options.InterfacePath)\IUpdate$($options.Name).cs" -Force;
Invoke-CodeGenerator -Option $classOption -n csharp.crud.get.interface -Output "$($options.InterfacePath)\IGet$($options.Name).cs" -Force;
Invoke-CodeGenerator -Option $classOption -n csharp.crud.delete.interface -Output "$($options.InterfacePath)\IDelete$($options.Name).cs" -Force;
Invoke-CodeGenerator -Option $classOption -n csharp.crud.list.interface -Output "$($options.InterfacePath)\IListe$($options.Name).cs" -Force;


$classOption = new-CSharpClassOption -name $options.Name -namespace Albatross.AccessControl -imports System,System.Collections.Generic,Albatross.AccessControl.Core,Dapper,System.Data -typecasts @{ PrincipalType = "int" } -inheritance "ICreate$($options.Name)";
$source = new-crudOperation -t $table -p $procedure
Invoke-CodeGenerator -s $source -Option $classOption -n csharp.crud.create -Output "$($options.ImplementationPath)\Create$($classOption.Name).cs" -Force;

$classOption = new-CSharpClassOption -name $options.Name -namespace Albatross.AccessControl -imports System,System.Collections.Generic,Albatross.AccessControl.Core,Dapper,System.Data -typecasts @{ PrincipalType = "int" };
$source = new-crudOperation -t $table -p $procedure
Invoke-CodeGenerator -s $source -Option $classOption -n csharp.crud.update -Output "$($options.ImplementationPath)\Update$($classOption.Name).cs" -Force;

#$classOption = new-CSharpClassOption -name $options.Name -namespace Albatross.AccessControl -imports System,System.Collections.Generic,Albatross.AccessControl.Core,Dapper,System.Data -typecasts @{ PrincipalType = "int" };
#$source = new-crudOperation -t $table -p $procedure
#Invoke-CodeGenerator -s $source -Option $classOption -n csharp.crud.get -Output "$($options.ImplementationPath)\Create$($classOption.Name).cs" -Force;

#$classOption = new-CSharpClassOption -name $options.Name -namespace Albatross.AccessControl -imports System,System.Collections.Generic,Albatross.AccessControl.Core,Dapper,System.Data -typecasts @{ PrincipalType = "int" };
#$source = new-crudOperation -t $table -p $procedure
#Invoke-CodeGenerator -s $source -Option $classOption -n csharp.crud.delete -Output "$($options.ImplementationPath)\Create$($classOption.Name).cs" -Force;

# Create Implementations