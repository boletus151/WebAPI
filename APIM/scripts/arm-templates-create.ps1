<#
.SYNOPSIS
    Create new ARM Templates based on the configuration file
    The folder where the templates are going to be created is going to be named as it is defined in the configuration file
.DESCRIPTION
    Arguments:
    -toolPath: The path where the tool is located
    -configFile: The path where the configuration file is located

#>
[CmdletBinding()]
param (
    [Parameter(Mandatory = $false, HelpMessage = "Displays the help content for this script.")]
    [switch]$help,
    [string]$toolPath,
    [string]$configFile
)

if (!$toolPath -or !$configFile) {
    $scriptName = $MyInvocation.MyCommand
    $script = Join-Path -Path $PSScriptRoot -ChildPath $scriptName
    Get-Help $script
    exit -1
}

if ($help) {
    Get-Help $PSCmdlet.MyInvocation.InvocationName -Detailed
    exit 0
}

try {
    $currentPath = Get-Location
    $toolPath = (Get-Item $toolPath ).FullName
    $configFile = (Get-Item $configFile ).FullName
    
    $functionResult = 0
    if (Test-Path $toolPath) {
        if (Test-Path $configFile) {
                
            $command = "ArmTemplates.exe create --configFile $configFile"
            $execute = ($toolPath, $command) -join "/"                
            Write-Host "Executing ---> $execute"

            $creatorResult = Invoke-Expression $execute

            $containsError = $creatorResult | Select-String "error"
            $containsException = $creatorResult | Select-String "exception"

            if ($containsError -or $containsException) {
                Write-Error "ARM Creator tool found an error."
                $functionResult = 1
            }
            else {
                Write-Host -ForegroundColor Green "ARM Templates created successfully `n"
            }
        }    
        else {
            Write-Error "Configuration file not found"
            $functionResult = 1
        }
    
    }    
    else {
        Write-Error "ARM Template Creator wrong path"
        $functionResult = 1
    }
}
catch {
    Write-Host "An exception occurred: $($_.Exception.Message) `n"
    Write-Error "--Variables not valid--"
    Write-Host $currentPath
    Write-Host $toolPath
    Write-Host $configFile
}

# AzurePowershellTask required
if ($functionResult -ne 0) {
    throw
}
else {
    exit $functionResult
}