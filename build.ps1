# Quick Script to run the dotnet watch command for the server project

Write-Output "Building the Server Project"

# Variables
$SolutionDir = "C:\Dev\AutoLazer"
$StartupProjectDir = "src\AutoLazer.Server"
$Path = Join-Path -Path $SolutionDir -ChildPath $StartupProjectDir

Set-Location -Path $SolutionDir

# Build and restore the application
dotnet restore
dotnet build