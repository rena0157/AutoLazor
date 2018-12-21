# Quick script that runs all of the projects in the solution
$testProjects = Get-ChildItem -Path "*.Tests.csproj" -Recurse

foreach ($project in $testProjects) {
    dotnet test $project.FullName
}