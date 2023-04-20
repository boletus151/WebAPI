<#
.SYNOPSIS
    Create new ARM Templates Specs in Azure
.DESCRIPTION
    Arguments:
    -templatesPath: The path where the arm templates are located. Required.
    -resourceGroup: The resource group name to deploy the templates. Required.
    -version: The template spec version. Optional.
    -isNew: If the template spec is new, you need to specify the location. Optional.
    -location: The location where the template spec will be created. Optional.

#>

[CmdletBinding()]
param (
    [Parameter(Mandatory = $false, HelpMessage = "Displays the help content for this script.")]
    [switch]$help,
    [string]$templatesPath,
    [string]$resourceGroup,
    [string]$version,
    [Parameter(Mandatory = $false)]
    [bool]$isNew,
    [Parameter(Mandatory = $false)]
    [string]$location
)


function Show-Help {
    param()
    Get-Help $PSCmdlet.MyInvocation.InvocationName -Detailed
}

if (!$templatesPath -or !$resourceGroup -or !$version) {
    $scriptName = $MyInvocation.MyCommand
    $script = Join-Path -Path $PSScriptRoot -ChildPath $scriptName
    Get-Help $script
    exit -1
}

if ($help) {
    Show-Help
    exit 0
}

$templatesPath = (Get-Item $templatesPath ).FullName
$files = Get-ChildItem $templatesPath | Where-Object { $_.Name -NotLike "*parameters*" } | Where-Object { $_.Name -NotLike "*master*" }
$functionResult = 0
foreach ($file in $files) {
    $name = [System.IO.Path]::GetFileNameWithoutExtension($file.FullName)
    try {                        
        $template = $file.FullName

        # $execute = "az ts create --name $name --display-name $name --version $version --resource-group $resourceGroup --template-file $template --yes"
        $execute = "New-AzTemplateSpec -Name $name -DisplayName $name -Version $version -ResourceGroupName $resourceGroup -TemplateFile $template -Force"
        if ($isNew -eq $true) {
            $execute += " -Location $location"
        }
        Write-Host "Executing ---> $execute"
        $result = Invoke-Expression $execute
                
        if (!$result) {    
            Write-Error "Check if the template spec $name already exists, if not, you need to create it first specifiying the location."
            $functionResult = -1                 
        }
        else { 
            Write-Host -ForegroundColor Green "Template uploaded : $name `n" 
        }
    }
    catch {
        Write-Host "An exception occurred: $($_.Exception.Message) `n"
        Write-Host
        $functionResult = -1
    }
}

Write-Host "Function Result: $functionResult"

# AzurePowershellTask required
if ($functionResult -ne 0) {
    throw
}
else {
    exit $functionResult
}
