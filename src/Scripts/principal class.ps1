& $PSScriptRoot\setup.ps1
Register-Assembly;

$table = Get-DatabaseTable -DbName ac -Criteria ac.Principal;
invoke-codegenerator -name sql.table.class -source $table -option $option;