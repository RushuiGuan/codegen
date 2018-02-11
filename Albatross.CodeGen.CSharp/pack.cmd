nuget pack Albatross.CodeGen.CSharp.nuspec -includeReferencedProjects -properties Configuration=Release
nuget push Albatross.CodeGen.CSharp.1.1.6.nupkg -source  https://api.nuget.org/v3/index.json
