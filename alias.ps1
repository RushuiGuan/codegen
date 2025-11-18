if ($IsMacOS) {
	set-alias -n codegen -v "$env:InstallDirectory/Albatross.CodeGen.CommandLine/codegen";
}
else {
	set-alias -n codegen -v "$env:InstallDirectory/Albatross.CodeGen.CommandLine/codegen.exe";
}