nuget pack Albatross.CodeGen.Database.nuspec -includeReferencedProjects -properties Configuration=Release
nuget push Albatross.CodeGen.Database.1.1.2.nupkg -source  https://api.nuget.org/v3/index.json
