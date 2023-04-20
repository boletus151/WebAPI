<#
.SYNOPSIS
    Validate ARM templates
.DESCRIPTION
    Arguments:
    -resourceGroup: The resource group name to deploy the templates
    -templatesPath:The path where the templates are located
    -parameters: The path where the parameters file is located

#>
[CmdletBinding()]
param (
    [Parameter(Mandatory = $false, HelpMessage = "Displays the help content for this script.")]
    [switch]$help,
    [string]$resourceGroup, ## Resource group name to deploy the templates
    [string]$templatesPath,
    [string]$parameters
)
function Show-Help {
    param()
    Get-Help $PSCmdlet.MyInvocation.InvocationName -Detailed
}

if (!$resourceGroup -or !$templatesPath -or !$parameters) {
    $scriptName = $MyInvocation.MyCommand
    $script = Join-Path -Path $PSScriptRoot -ChildPath $scriptName
    Get-Help $script
    exit -1
}

if ($help) {
    Show-Help
    exit 0
}

$files = Get-ChildItem -Path $templatesPath | Where-Object { $_.Name -NotLike "*parameters*" }
                
Write-Host "ARM Templates found:"
Write-Host "--------------------"
foreach ($file in $files) {
    Write-Host $file.Name 
}
Write-Host

$containsError = $false
$functionResult = 0

foreach ($file in $files) {
    $name = $file.Name
    try {                        
        $template = $file.FullName
        $execute = "Test-AzResourceGroupDeployment  -SkipTemplateParameterPrompt -ResourceGroupName $resourceGroup -TemplateFile $template -TemplateParameterFile $parameters"
        Write-Host "Executing ---> $execute"
        $result = Invoke-Expression $execute
                            
        $containsError = $result | Select-String "error"
                        
        if ($result) {
            if ($result[0].Message -match "he only supported parameters for this template are") {          
                # Creator tool does not include all parameters in all the template its create,
                # so missing parameters are not going to be considered as an error validating the template
                Write-Host -ForegroundColor Green "Valid template : $name `n"                     
            }
            else {
                if ($containsError -or $result.Exception) {    
                    Write-Error $result[0].Code ": " $name
                    Write-Host $result[0].Message `n
                    $functionResult = 1
                }
                else { 
                    Write-Host -ForegroundColor Green "Valid template : $name `n" 
                }
            }
        }
        else { 
            Write-Host -ForegroundColor Green "Valid template : $name `n" 
        }
    }
    catch {
        Write-Host "An exception occurred: $($_.Exception.Message) `n"
        Write-Error "$name template not valid"
        Write-Host
        $functionResult = 1
    }
}
    
# AzurePowershellTask required
if ($functionResult -ne 0) {
    throw
}
else {
    exit $functionResult
}