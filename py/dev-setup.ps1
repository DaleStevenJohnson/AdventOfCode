<#
.SYNOPSIS
    Sets up a Python virtual environment and installs requirements.

.DESCRIPTION
    Checks for an existing venv. If found, exits early unless -Recreate is used.
    Creates a new venv, updates pip, and installs requirements.txt.

.PARAMETER VenvName
    Name of the virtual environment folder. Default: ".venv"

.PARAMETER ReqFile
    Path to requirements file. Default: "requirements.txt"

.PARAMETER Recreate
    Switch to force delete and recreate the environment.

.EXAMPLE
    ./setup_venv.ps1
.EXAMPLE
    ./setup_venv.ps1 -Recreate
.EXAMPLE
    ./setup_venv.ps1 -VenvName "my_env" -ReqFile "dev-requirements.txt"
#>

param (
    [string]$VenvName = ".venv",
    [string]$ReqFile = "requirements.txt",
    [switch]$Recreate
)

$ErrorActionPreference = "Stop"

# 1. Check if Python is installed
try {
    $null = Get-Command "python" -ErrorAction Stop
}
catch {
    Write-Error "Python is not installed or not found in PATH."
    exit 1
}

# 2. Check for existing Environment
if (Test-Path $VenvName) {
    if ($Recreate) {
        Write-Host "[-] Removing existing virtual environment '$VenvName'..." -ForegroundColor Yellow
        Remove-Item -Path $VenvName -Recurse -Force
    }
    else {
        Write-Host "[!] Virtual environment '$VenvName' already exists." -ForegroundColor Cyan
        Write-Host "    Use -Recreate to force a fresh install." -ForegroundColor Gray
        
        # Check if we should exit or if the user just wanted to ensure it exists
        # To strictly follow prompt "exit early":
        exit 0
    }
}

# 3. Create the Environment
Write-Host "[+] Creating virtual environment '$VenvName'..." -ForegroundColor Green
try {
    python -m venv $VenvName
}
catch {
    Write-Error "Failed to create virtual environment."
    exit 1
}

# 4. Locate the Python executable inside the venv
# On Windows, it is usually inside /Scripts. On Linux/Mac, inside /bin.
$VenvPython = Join-Path $VenvName "Scripts\python.exe"
if (-not (Test-Path $VenvPython)) {
    # Fallback for cross-platform compatibility
    $VenvPython = Join-Path $VenvName "bin/python"
}

if (-not (Test-Path $VenvPython)) {
    Write-Error "Could not locate python executable inside the new virtual environment."
    exit 1
}

# 5. Upgrade pip (Best Practice)
Write-Host "[*] Upgrading pip..." -ForegroundColor Cyan
& $VenvPython -m pip install --upgrade pip | Out-Null

# 6. Install Requirements
if (Test-Path $ReqFile) {
    Write-Host "[+] Installing requirements from $ReqFile..." -ForegroundColor Green
    & $VenvPython -m pip install -r $ReqFile
}
else {
    Write-Warning "File '$ReqFile' not found. Skipping dependency installation."
}

# 7. Activation logic
# Note: We cannot persist activation for the parent shell from a child script 
# unless the script is dot-sourced (. ./script.ps1).
# We attempt to activate it for the current session scope if dot-sourced.

$ActivateScript = Join-Path $VenvName "Scripts\Activate.ps1"

if (Test-Path $ActivateScript) {
    Write-Host "[*] Setup complete." -ForegroundColor Green
    
    # Try to activate in current scope
    try {
        . $ActivateScript
        Write-Host "[+] Virtual Environment Activated!" -ForegroundColor Magenta
    }
    catch {
        Write-Warning "Could not activate automatically. Run: $ActivateScript"
    }
}