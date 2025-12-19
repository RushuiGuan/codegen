get-item $PSScriptRoot/*.generated.cs | remove-item;

$location = Get-Location

try {
	set-Location $PSScriptRoot/../Albatross.CodeGen.CommandLine

	# dotnet run --no-launch-profile -- schema csharp --file $PSScriptRoot/codegen-settings.schema.json
	
	dotnet run --no-launch-profile -- csharp2 web-client `
		-p $PSScriptRoot/../Test.WebApi/Test.WebApi.csproj `
		-s $PSScriptRoot/codegen-settings.json `
		-o $PSScriptRoot/
	dotnet format $PSScriptRoot/Test.Proxy.csproj --include-generated
} finally {
	set-Location $location
}