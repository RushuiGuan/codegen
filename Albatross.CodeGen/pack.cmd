nuget pack Albatross.CodeGen.nuspec -includeReferencedProjects -properties Configuration=Release
nuget push Albatross.CodeGen.1.1.3.nupkg -source  https://api.nuget.org/v3/index.json
