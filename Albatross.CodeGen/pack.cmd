nuget pack Albatross.CodeGen.nuspec -includeReferencedProjects -properties Configuration=Release
copy Albatross.CodeGen.*.nupkg c:\workspace\LocalNuGetPackages\

nuget push Albatross.CodeGen.*.nupkg -source  https://api.nuget.org/v3/index.json