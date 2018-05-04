& $PSScriptRoot\setup.ps1
Register-Assembly;

Get-DatabaseTable -DbName ac -Criteria ac.% | Sort-Object -Property Name | ForEach-Object{
	$user = New-SqlParameter -name user -type "varchar(100)";

    $option = New-SqlQueryOption -Name "Create$($_.Name)" -Schema "ac" -Expressions @{
            "@created" = "sysutcdatetime()"; 
            "@modified" ="sysutcdatetime()";
            "@createdBy" = "@user";
            "@modifiedBy" = "@user";
        } -Parameter $user -GrantPermission -Principals app_svc;

    New-Branch -Nodes sql.procedure, sql.insert, newline, sql.select.identity | Invoke-Composite -Source $_ -Option $option;
}

