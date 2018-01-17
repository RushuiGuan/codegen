$db = New-DatabaseServer -DataSource . -InitialCatalog albatross -IntegratedSecurity
New-Composite -Name test_composite -Category test -Target sql -Generators a,b,c | Set-Composite;
