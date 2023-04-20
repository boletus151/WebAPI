# common
$currentPath = (Split-Path ($PSScriptRoot) -Parent)

$toolPath = Join-Path -Path $currentPath -ChildPath "ArmTemplatesTool"
$resourceGroup = "rg-webapi"

# arm-templates-validate
$templatesPath = Join-Path -Path $currentPath -ChildPath "ArmTemplates\dev-current"
$templatesPathQA = Join-Path -Path $currentPath -ChildPath "ArmTemplates\qa-current"
$parameters = Join-Path -Path $currentPath -ChildPath "ArmTemplates\parameters-dev.json"
$parametersQA = Join-Path -Path $currentPath -ChildPath "ArmTemplates\parameters-qa.json"

# arm-templates-create
$configFile = Join-Path -Path $currentPath -ChildPath "configuration.dev-v1.yml"
$configFileQA = Join-Path -Path $currentPath -ChildPath "configuration.qa-v1.yml"
$version = "1"
$location = "francecentral"
$isNew = $false
# $isNew = $true

Write-Host "currentPath: $currentPath"
Write-Host "toolPath: $toolPath"
Write-Host "resourceGroup: $resourceGroup"
Write-Host "templatesPath: $templatesPath"
Write-Host "templatesPath QA: $templatesPathQA"
Write-Host "parameters: $parameters"
Write-Host "parameters QA: $parametersQA"
Write-Host "configFile: $configFile"
Write-Host "configFile QA: $configFileQA"
Write-Host "version: $version"
Write-Host "location: $location"
Write-Host "isNew: $isNew"

Write-Host ""

# deployments
$template = Join-Path -Path $currentPath -ChildPath "ArmTemplates\master.template-dev.json"
$templateQA = Join-Path -Path $currentPath -ChildPath "ArmTemplates\master.template-qa.json"
Write-Host "deployment template: $template"
Write-Host "deployment template QA: $templateQA"

Write-Host "" 