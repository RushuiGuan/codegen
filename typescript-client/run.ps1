$location = get-location;
$project = $PSScriptRoot;

get-item $project/projects/test-webclient/src/lib/*.generated.ts | remove-item;
set-location $PSScriptRoot/../Albatross.CodeGen.CommandLine;


dotnet run --no-launch-profile -- schema typescript --file $PSScriptRoot/codegen-settings.schema.json
dotnet run -- typescript dto `
	-p $project/../Test.Dto/Test.Dto.csproj `
	-s $PSScriptRoot/codegen-settings.json `
	-o $project/projects/test-webclient/src/lib/ `
	-v information


dotnet run -- typescript web-client `
	-p $project/../Test.WebApi/Test.WebApi.csproj `
	-s $PSScriptRoot/codegen-settings.json `
	-o $project/projects/test-webclient/src/lib/ `
	-v information `
	--show-stack

# dotnet run -- typescript entrypoint `
# 	-s $PSScriptRoot/codegen-settings.json `
# 	-o $project/projects/test-webclient/src/ `
# 	-v information

Set-Location $location;