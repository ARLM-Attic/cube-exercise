$toolsDir = [IO.Path]::GetDirectoryName($MyInvocation.InvocationName)
$toolsDir = Convert-Path $toolsDir
Push-Location $toolsDir
$publicincDir = Join-Path -Path $toolsDir -ChildPath "..\public\inc"
$publicincDir = Convert-Path $publicincDir

.\beaver.ps1 -verfile (Join-Path -Path $publicincDir -ChildPath "version.txt") -source (Join-Path -Path $publicincDir -ChildPath "bldvercs.template") -target (Join-Path -Path $publicincDir -ChildPath "bldver.cs")
.\beaver.ps1 -verfile (Join-Path -Path $publicincDir -ChildPath "version.txt") -source (Join-Path -Path $publicincDir -ChildPath "bldverh.template") -target (Join-Path -Path $publicincDir -ChildPath "bldver.h")

Pop-Location