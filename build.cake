#tool nuget:?package=vswhere

var target = Argument("Target", "Default");
var configuration = Argument("configuration", "Release");
var destDirectory = Directory("./ClickType/bin");

DirectoryPath vsLatest  = VSWhereLatest();
FilePath msBuildPathX64 = (vsLatest==null)
                            ? null
                            : vsLatest.CombineWithFilePath("./MSBuild/Current/Bin/MSBuild.exe");

Task("Clean").Does(() => {
    CleanDirectory(destDirectory);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./ClickType.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {
        var msbuildSettings = new MSBuildSettings()    
                                    .WithProperty("TreatWarningsAsErrors","false")
                                    .WithTarget("Build")
                                    .SetConfiguration("Release");
        msbuildSettings.ToolPath = msBuildPathX64;

        MSBuild("./ClickType.sln", msbuildSettings);
    }
    else
    {
        XBuild("./ClickType.sln", settings =>
            settings.SetConfiguration(configuration));
    }
});

Task("Default").IsDependentOn("Build");

RunTarget(target);
