nuget pack Albatross.CodeGen.PowerShell.nuspec -properties Configuration=Release
nuget push Albatross.CodeGen.PowerShell.1.1.7.nupkg -source  https://api.nuget.org/v3/index.json
