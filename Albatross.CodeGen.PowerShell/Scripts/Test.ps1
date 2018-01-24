

get-location | set-CompositeLocation;
get-CompositeLocation;

get-location | set-ScenarioLocation;
get-ScenarioLocation;


$comp = New-Composite -name UpdateWithIdentity -generator table_update,table_where_byid -target sql -st Albatross.CodeGen.Database.Table;
Set-Composite $comp .\UpdateWithIdentity.composite 


