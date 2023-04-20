param (
    [string]$env,
    [string]$resourceGroup,
    [string]$output
)

$filter = "-" + $env

Write-Host "Get all Template Specs filtered by $filter and store them in $output folder:"
Write-Host "---------------------------------------------------------------"
$names = (az ts list -g $resourceGroup --query  "[?contains(name, '$filter')].name" --output 'json') | ConvertFrom-Json

if ($nam) {
    Write-Host "There are no templates that contains the filter: $filter" -ForegroundColor Red
    exit -1
}

if (!(Test-Path $output)) {
    New-Item -ItemType Directory -Path $output
    Write-Host "Folder created"
}
else {
    Write-Host "The folder already exists."
}

foreach ($name in $names) {
    $versions = (az ts show -g $resourceGroup --name $name --query "versions") | ConvertFrom-Json
    $latest = $versions[-1]

    $fail = $false
    try {
        Write-Host "Downloading $name template version $latest"
        az ts export --output-folder $output --name $name --resource-group $resourceGroup --version $latest
        Write-Host "$name template successfully downloaded" -ForegroundColor Green
    }
    catch {      
        $fail = $true      
        Write-Host "There are no templates that contains the filter: $filter" -ForegroundColor Red
    }
}

if ($fail) {
    return -1 
}
else {
    Get-ChildItem $output
    return 0
}