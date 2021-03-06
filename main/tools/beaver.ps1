

# Parameters declaration
param(
    [string]$verfile,
    [string]$scheme = "Default",
    [string]$format,
    [string]$source,
    [string]$target,
    [switch]$local,
    [switch]$inc,
    [switch]$dot,
    [switch]$whatIf,
    [switch]$help)

#verfile:filename
# A text file with the full product version and build number, such as:
#1.1.305.0
#The format is:
#ProductMajor.ProductMinor.BuildMajor.BuildMinor
# 
#scheme:string
# An optional parameter which affects the behavior of the inc option. The possible values are default, date, date-qfe,  eightdigitdate, sixdigitdate, and fivedigitdate.
#Default specifies that the BuildMajor value is simply incremented.
#Date specifies that the first two digits of the BuildMajor value are a month counter and the second two digits are the day of the month. The month counter starts out as the current month number, but is incremented by 12 months at a time to ensure that the new build number is greater than or equal to the previous build number. For example, if you do builds every day, the build after 1231 will be 1301.
#Date-qfe is similar to date except that a four digit BuildMinor is required, and the first two digits of BuildMinor are reserved for a QFE number and never changed by beaver.
#Eightdigitdate or sixdigitdate will set BuildMajor to the current date in MMDDYYYY or MMDDYY format, respectively.
#Fivedigitdate will set BuildMajor to the current date in YMMDD format.
#When using any of the date based schemes, the first build of the day will set the BuildMinor to 0 and subsequent builds on the same day will increment BuildMinor. 
# 
#local
# An optional parameter. When present, beaver will use a local date instead of a UTC date for all date based schemes.
#
#format:string
# An optional parameter. When present, the current version will be formatted to the standard output. The argument may contain the special format fields described below or any other environment variable.
#
#target:filename
# A .h file to create. This will be formatted according to the contents of the file specified by the hf: parameter that must be present.
#
#source:filename
# Template filename that contains the format for the header: processing. Format variables and environment variables are replaced just as with the format: option.
#
#inc
# Increment the build major number before producing any output or file.
# 
#dot
# Increment the build minor number before producing any output or file.
#
 
function ErrorExit([string]$message="Unknown Error", [int]$errorCode=1)
{
    Write-Error $message
    Exit $errorCode
}

function CheckPrerequisites
{
    if (!(Test-Path "$verfile" -pathType Leaf))
    {
        ErrorExit -message "The version file '$verfile' is not available." -errorCode 1
    }
    
    if (!(Test-Path "$source" -pathType Leaf))
    {
        ErrorExit -message "The source template file '$source' is not available." -errorCode 1
    }
}


# The script execution starts here.
if ($help)
{
    PrintHelp
    Exit 0
}

CheckPrerequisites

$versionContent = Get-Content -Path "$verfile" -TotalCount  1
#ProductMajor.ProductMinor.BuildMajor.BuildMinor
$version = $versionContent.Split('.')
#Verify the length of string array $version (should be exactly 4)
$ProductMajor = $version[0]
$ProductMinor = $version[1]
$BuildMajor = $version[2]
$BuildMinor = $version[3]
$ProductMajorNumber = $ProductMajor.TrimStart('0')
$ProductMinorNumber = $ProductMinor.TrimStart('0')
$BuildMajorNumber = $BuildMajor.TrimStart('0')
$BuildMinorNumber = $BuildMinor.TrimStart('0')

if ("$ProductMajorNumber" -eq "")
{
    $ProductMajorNumber = "0"
}

if ("$ProductMinorNumber" -eq "")
{
    $ProductMinorNumber = "0"
}

if ("$BuildMajorNumber" -eq "")
{
    $BuildMajorNumber = "0"
}

if ("$BuildMinorNumber" -eq "")
{
    $BuildMinorNumber = "0"
}

Push-Location Env:\
New-Item -Name ProductMajor -Value $ProductMajor -Force
New-Item -Name ProductMinor -Value $ProductMinor -Force
New-Item -Name BuildMajor -Value $BuildMajor -Force
New-Item -Name BuildMinor -Value $BuildMinor -Force
New-Item -Name ProductMajorNumber -Value $ProductMajorNumber -Force
New-Item -Name ProductMinorNumber -Value $ProductMinorNumber -Force
New-Item -Name BuildMajorNumber -Value $BuildMajorNumber -Force
New-Item -Name BuildMinorNumber -Value $BuildMinorNumber -Force
Pop-Location

if (Test-Path "$target" -pathType Leaf)
{
    Clear-Content -Path "$target"
}

$newLine = [Environment]::NewLine
$templateContent = Get-Content -Path "$source"
foreach ($line in $templateContent)
{
    $expanded = [Environment]::ExpandEnvironmentVariables($line)
    Add-Content -Path "$target" -Value "$expanded"
}

