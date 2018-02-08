nuget pack Albatross.CodeGen.Database.nuspec -includeReferencedProjects -properties Configuration=Release
copy Albatross.CodeGen.*.nupkg c:\workspace\LocalNuGetPackages\

nuget push Albatross.CodeGen.Database.1.1.0.nupkg -source  https://api.nuget.org/v3/index.json