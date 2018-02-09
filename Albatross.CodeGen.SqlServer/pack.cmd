nuget pack Albatross.CodeGen.SqlServer.nuspec -includeReferencedProjects -properties Configuration=Release
nuget push Albatross.CodeGen.SqlServer.1.1.2.nupkg -source  https://api.nuget.org/v3/index.json
