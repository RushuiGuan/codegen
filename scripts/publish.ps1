Import-Module app-dev

$app_root = (get-item $PSScriptRoot).Directory;

set-directory $app_root, scripts, code-gen, bin;
