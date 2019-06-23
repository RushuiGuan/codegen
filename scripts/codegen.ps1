function Invoke-ClassLoader {
	param(
		[Parameter(Mandatory)]
		[System.IO.DirectoryInfo[]]$assemblyFolders,	

		[Parameter(Mandatory)]
		[System.IO.FileInfo]$targetAssembly,

		[Parameter(Mandatory)]
		[string]$converterTypeName,
		
		[string]$pattern
	)
	
	$items = @();
	
	$assemblyFolders | ForEach-Object { $items += $_.FullName; }
	$assemblyPath = $items -join ",";
	$cmd = "$PSscriptRoot\classloader\classLoader.exe -a `"$assemblyPath`" -t $($targetAssembly.FullName) -c $converterTypeName";
	if ($pattern) {
		$cmd = "$cmd -p `"$pattern`"";
	}
	Invoke-Expression $cmd;
}