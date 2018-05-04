& $PSScriptRoot\setup.ps1
Register-Assembly;

Get-DatabaseTable -DbName ac -Criteria ac.% | Sort-Object -Property Name | ForEach-Object{
    $userVariable = New-Object Albatross.Database.Variable;
    $userVariable.Name = "@user";
    $userVariable.Type = New-Object Albatross.Database.SqlType
    $userVariable.Type.Name = "varchar";
    $userVariable.Type.MaxLength = 100;

    $option = New-SqlQueryOption -Name "Create$($_.Name)" -Schema "ac" -Expressions @{
            "@created" = "sysutcdatetime()"; 
            "@modified" ="sysutcdatetime()";
            "@createdBy" = "@user";
            "@modifiedBy" = "@user";
        } -Variables $userVariable;
    New-Branch -Nodes "create stored procedure", table_insert | Invoke-Composite -Source $_ -Option $option;
}

