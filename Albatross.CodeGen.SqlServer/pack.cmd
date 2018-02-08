nuget pack Albatross.CodeGen.SqlServer.nuspec -includeReferencedProjects -properties Configuration=Release
copy Albatross.CodeGen.*.nupkg c:\workspace\LocalNuGetPackages\

nuget push Albatross.CodeGen.SqlServer.1.1.0.nupkg -source  https://api.nuget.org/v3/index.json