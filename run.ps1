.\build.ps1

$SolutionDir = "C:\Dev\AutoLazer"
$StartupProjectDir = "src\AutoLazer.Server"
$Path = Join-Path -Path $SolutionDir -ChildPath $StartupProjectDir

# move the shell to the location of the server app and run
Set-Location -Path $Path
dotnet watch run

Write-Output "Exiting..."
Set-Location -Path $SolutionDir