nuget pack Albatross.CodeGen.PowerShell.nuspec -properties Configuration=Release
copy Albatross.CodeGen.*.nupkg c:\workspace\LocalNuGetPackages\


rem nuget push Albatross.CodeGen.PowerShell.1.0.1-alpha.nupkg -source  https://api.nuget.org/v3/index.json