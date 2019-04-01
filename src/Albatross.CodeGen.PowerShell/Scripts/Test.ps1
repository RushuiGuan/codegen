cls
Set-Alias -Name classloader -Value $PSScriptRoot\..\..\Albatross.CodeGen.ClassLoader\bin\publish\ClassLoader.exe
import-module $PSScriptRoot\..\bin\Debug\net46\Albatross.CodeGen.PowerShell.dll;
classloader -f $PSScriptRoot\..\bin\Debug\net46\Albatross.CodeGen.dll | Read-JsonObject -Type Albatross.CodeGen.CSharp.Model.Class -Array | Invoke-CSharpClassGenerator
