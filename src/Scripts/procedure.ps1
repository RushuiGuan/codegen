cls
Get-StoredProcedure -DbName albatross -Criteria sec.% | ForEach-Object{
    Invoke-CodeGenerator -Name "procedure command definition" -Source $_
}
