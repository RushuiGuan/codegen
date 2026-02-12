get-item $PSScriptRoot/*.generated.cs | remove-item;

$location = Get-Location

try {
	set-Location $PSScriptRoot/../Albatross.CodeGen.CommandLine

	# dotnet run --no-launch-profile -- 
	codegen schema csharp --output-file $PSScriptRoot/codegen-settings.schema.json

	# codegen csharp web-client `
	dotnet run --no-launch-profile -- csharp web-client `
		-p $PSScriptRoot/../Test.WebApi/Test.WebApi.csproj `
		-s $PSScriptRoot/codegen-settings.json `
		-o $PSScriptRoot/ `
		-v Info

	dotnet format $PSScriptRoot/Test.Client.csproj --include-generated
} finally {
	set-Location $location
}