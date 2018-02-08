nuget pack Albatross.CodeGen.nuspec -includeReferencedProjects -properties Configuration=Release
nuget push Albatross.CodeGen.1.1.1.nupkg -source  https://api.nuget.org/v3/index.json