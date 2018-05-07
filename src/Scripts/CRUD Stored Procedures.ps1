cls
& $PSScriptRoot\setup.ps1
Register-Assembly;

Get-DatabaseTable -DbName ac -Criteria ac.% | Where-Object -Property IdentityColumn -NE $null | Sort-Object -Property Name | ForEach-Object{
	[string]$schema = "ac";
	[string]$name = "Create$($_.Name)";
	
    $createOption = New-SqlQueryOption -Name $name -Schema $schema -Filter ByIdentityColumn;
	$alterOption = New-SqlQueryOption -Name $name -Schema $schema -Filter ByIdentityColumn -AlterProcedure;
	[Albatross.Database.Procedure]$procedure = New-StoredProcedure -name $name -schema $schema -database $_.Database 
	$procedure.CreateScript = Invoke-Composite -Source $_ -Option $createOption -b sql.procedure, sql.insert, newline, sql.select.identity;
	$procedure.AlterScript = Invoke-Composite -Source $_ -Option $alterOption -b sql.procedure, sql.insert, newline, sql.select.identity;
	$procedure.Permissions = ,(new-databasePermission -state grant -permission execute -principal app_svc);
	publish-procedure $procedure;

    
    $option = New-SqlQueryOption -Name "Update$($_.Name)" -Schema "ac" -Filter ByIdentityColumn;
    New-Branch -Nodes sql.procedure, sql.update, newline, sql.where.table | Invoke-Composite -Source $_ -Option $option;

    $option = New-SqlQueryOption -Name "Delete$($_.Name)" -Schema "ac" -Filter ByIdentityColumn;
    New-Branch -Nodes sql.procedure, sql.delete, newline, sql.where.table | Invoke-Composite -Source $_ -Option $option;

    $option = New-SqlQueryOption -Name "Get$($_.Name)" -Schema "ac" -Filter ByIdentityColumn;
    New-Branch -Nodes sql.procedure, sql.select.table, newline, sql.where.table | Invoke-Composite -Source $_ -Option $option;
}


<#
Get-DatabaseTable -DbName ac -Criteria ac.% | Where-Object -Property IdentityColumn -EQ $null | Sort-Object -Property Name | ForEach-Object{
	$user = New-SqlParameter -name user -type "varchar(100)";

    $option = New-SqlQueryOption -Name "Create$($_.Name)" -Schema "ac" -Expressions @{
            "@created" = "sysutcdatetime()"; 
            "@modified" ="sysutcdatetime()";
            "@createdBy" = "@user";
            "@modifiedBy" = "@user";
        } -Parameter $user -GrantPermission -Principals app_svc -Filter ByPrimaryKey;

    New-Branch -Nodes sql.procedure, sql.insert | Invoke-Composite -Source $_ -Option $option;
}
#>


