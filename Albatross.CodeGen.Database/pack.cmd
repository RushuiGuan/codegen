nuget pack Albatross.CodeGen.Database.nuspec -includeReferencedProjects -properties Configuration=Release

rem nuget push Albatross.CodeGen.SqlServer.1.0.0-alpha.nupkg -source  https://api.nuget.org/v3/index.json