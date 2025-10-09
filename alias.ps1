if ($IsMacOS) {
	set-alias -n codegen -v (Join $env:InstallDirectory, "Albatross.CodeGen.CommandLine", "codegen");
} else {
	set-alias -n codegen -v (Join $env:InstallDirectory, "Albatross.CodeGen.CommandLine", "codegen.exe");
}