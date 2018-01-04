nuget pack Albatross.CodeGen.nuspec -includeReferencedProjects -properties Configuration=Release

rem nuget push Albatross.CodeGen.*.nupkg -source  https://api.nuget.org/v3/index.json