///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var slnFile = @"src\FlaUI.Adapter.White.sln";
var artifactDir = new DirectoryPath("artifacts");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
});

Teardown(ctx =>
{
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(artifactDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(slnFile);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    var buildLogFile = artifactDir.CombineWithFilePath("BuildLog.txt");
    var buildSettings = new MSBuildSettings {
        Verbosity = Verbosity.Minimal,
        Configuration = configuration,
        PlatformTarget = PlatformTarget.MSIL,
    }.AddFileLogger(new MSBuildFileLogger {
        LogFile = buildLogFile.ToString(),
        MSBuildFileLoggerOutput = MSBuildFileLoggerOutput.All
    });
    MSBuild(slnFile, buildSettings);

    // Zip the logs
    Zip(artifactDir, artifactDir.CombineWithFilePath("BuildLog.zip"), new [] { buildLogFile });
});

Task("Push-To-Nuget")
    .Does(() =>
{
    var apiKey = System.IO.File.ReadAllText(".nugetapikey");

    // Get the paths to the packages
    var packages = GetFiles($"{artifactDir}/*.nupkg");

    // Push the packages
    foreach (var package in packages) {
        Information($"Pushing {package}");
        NuGetPush(package, new NuGetPushSettings {
            Source = "https://nuget.org/api/v2/package",
            ApiKey = apiKey
        });
    }
});

 Task("Push-To-SymbolSource")
    .Does(() =>
{
    var apiKey = System.IO.File.ReadAllText(".nugetapikey");

    // Get the paths to the packages
    var packages = GetFiles($"{artifactDir}/*.snupkg");

    // Push the packages
    foreach (var package in packages) {
        Information($"Pushing {package}");
        NuGetPush(package, new NuGetPushSettings {
            Source = "https://nuget.smbsrc.net",
            ApiKey = apiKey
        });
    }
 });

Task("Default")
    .Does(() => {
    Information("Hello Cake!");
});

RunTarget(target);
