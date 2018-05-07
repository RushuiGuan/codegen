& $PSScriptRoot\setup.ps1
Register-Assembly;

$location = Get-Item $PSScriptRoot\..\ac-db\principal;
$table = Get-DatabaseTable -DbName ac -Criteria ac.Principal;

$user_parameter = New-SqlParameter -name user -type "varchar(100)";
$option = New-SqlQueryOption -Name "Create$($table.Name)" -Schema "ac" -Expressions @{
    "@created" = "sysutcdatetime()"; 
    "@modified" ="sysutcdatetime()";
    "@createdBy" = "@user";
    "@modifiedBy" = "@user";
} -Parameter $user_parameter -GrantPermission -Principals app_svc -Filter ByIdentityColumn;
Invoke-Composite -Source $table -Option $option -b sql.procedure, sql.insert, newline, sql.select.identity -Output "$location\Create$($table.Name).sql" -Force;

$option = New-SqlQueryOption -Name "Update$($table.Name)" -Schema "ac" -Expressions @{
    "@modified" ="sysutcdatetime()";
    "@modifiedBy" = "@user";
} -Parameter $user_parameter -GrantPermission -Principals app_svc -Filter ByIdentityColumn;
Invoke-Composite -Source $table -Option $option -b sql.procedure, sql.update, newline, sql.where.table  -Output "$location\Update$($table.Name).sql" -Force;

$option = New-SqlQueryOption -Name "Delete$($table.Name)" -Schema "ac" -Parameter $user_parameter -GrantPermission -Principals app_svc -Filter ByIdentityColumn;
Invoke-Composite -Source $table -Option $option -b sql.procedure, sql.delete, newline, sql.where.table  -Output "$location\Delete$($table.Name).sql" -Force;

$option = New-SqlQueryOption -Name "Get$($table.Name)" -Schema "ac" -GrantPermission -Principals app_svc -Filter ByIdentityColumn;
Invoke-Composite -Source $table -Option $option -b sql.procedure, sql.select.table, newline, sql.where.table  -Output "$location\Get$($table.Name).sql" -Force;


<#
Get-DatabaseTable -DbName ac -Criteria ac.% | Where-Object -Property IdentityColumn -EQ $null | Sort-Object -Property Name | ForEach-Object{
	$user_parameter = New-SqlParameter -name user -type "varchar(100)";

    $option = New-SqlQueryOption -Name "Create$($table.Name)" -Schema "ac" -Expressions @{
            "@created" = "sysutcdatetime()"; 
            "@modified" ="sysutcdatetime()";
            "@createdBy" = "@user";
            "@modifiedBy" = "@user";
        } -Parameter $user_parameter -GrantPermission -Principals app_svc -Filter ByPrimaryKey;

    New-Branch -Nodes sql.procedure, sql.insert | Invoke-Composite -Source $table -Option $option;
}
#>


