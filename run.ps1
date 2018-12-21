# Quick Script to run the dotnet watch command for the server project

Write-Output "Running the Server Project"

# Variables
$SolutionDir = "C:\Dev\AutoLazer"
$StartupProjectDir = "src\AutoLazer.Server"
$Path = Join-Path -Path $SolutionDir -ChildPath $StartupProjectDir

Set-Location -Path $SolutionDir

# Build and restore the application
dotnet build
dotnet restore

# move the shell to the location of the server app and run
Set-Location -Path $Path
dotnet watch run

Write-Output "Exiting..."
Set-Location -Path $SolutionDir