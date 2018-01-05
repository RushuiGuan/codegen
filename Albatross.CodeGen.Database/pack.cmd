nuget pack Albatross.CodeGen.Database.nuspec -includeReferencedProjects -properties Configuration=Release
copy Albatross.CodeGen.*.nupkg c:\workspace\LocalNuGetPackages\

rem nuget push Albatross.CodeGen.SqlServer.1.0.0-alpha.nupkg -source  https://api.nuget.org/v3/index.json