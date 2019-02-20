#tool nuget:?package=vswhere

using Cake.Common.IO;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// SET PACKAGE VERSION
//////////////////////////////////////////////////////////////////////

var version = "5.4";
var modifier = "";

var dbgSuffix = configuration == "Debug" ? "-dbg" : "";
var packageVersion = version + modifier + dbgSuffix;

var packageName = "runsettings";

//////////////////////////////////////////////////////////////////////
// DEFINE RUN CONSTANTS
//////////////////////////////////////////////////////////////////////

// Directories
var PROJECT_DIR = Context.Environment.WorkingDirectory.FullPath + "/";
var ALLTEMPLATE = "AllTemplate/";
var COVERAGENOPARALLEL = "CoverageNoParallelTemplate/";
var PARALLEL = "ParallelTemplate/";

var ITEMTEMPLATESFOLDER = "ItemTemplates/";

var PACKAGE_DIR = PROJECT_DIR + "package/";
var PACKAGE_IMAGE_DIR = PACKAGE_DIR + packageName + "/";
var SRC_DIR = PROJECT_DIR + "src/";
var BIN_DIR = PROJECT_DIR + "bin/" + configuration + "/";

var ADAPTER_PROJECT = SRC_DIR + "NUnitTestAdapter/NUnit.TestAdapter.csproj";

var ADAPTER_BIN_DIR_NET35 = SRC_DIR + $"NUnitTestAdapter/bin/{configuration}/net35/";
var ADAPTER_BIN_DIR_NETCOREAPP10 = SRC_DIR + $"NUnitTestAdapter/bin/{configuration}/netcoreapp1.0/";

var BIN_DIRS = new [] {
	PROJECT_DIR + "src/empty-assembly/bin",
	PROJECT_DIR + "src/mock-assembly/bin",
	PROJECT_DIR + "src/NUnit3TestAdapterInstall/bin",
	PROJECT_DIR + "src/NUnit3TestAdapter/bin",
	PROJECT_DIR + "src/NUnit3TestAdapterTests/bin",
};



// Solution
var RUNSETTINGS_SOLUTION = PROJECT_DIR + "RunSettings.sln";


void CopyToArchive(string folder, string source, string target)
{
	 var files = GetFiles(PROJECT_DIR+folder+"*.vstemplate");
	 files.Add(PROJECT_DIR+folder+"STAR10.ico");
	 files.Add(PROJECT_DIR+folder+source);
	 Zip(PROJECT_DIR+folder,PROJECT_DIR+folder+target,files);


}


//////////////////////////////////////////////////////////////////////
// CLEAN
//////////////////////////////////////////////////////////////////////

Task("Clean")
	.Does(() =>
{
	foreach(var dir in BIN_DIRS)
		CleanDirectory(dir);
});

//////////////////////////////////////////////////////////////////////
// INITIALIZE FOR BUILD
//////////////////////////////////////////////////////////////////////

Task("NuGetRestore")
	.Does(() =>
{
	Information("Restoring NuGet Packages for the Adapter Solution");
	MSBuild(RUNSETTINGS_SOLUTION, new MSBuildSettings
	{
		Configuration = configuration,
		Verbosity = Verbosity.Minimal,
		ToolVersion = MSBuildToolVersion.VS2017
	}.WithTarget("Restore"));
});

//////////////////////////////////////////////////////////////////////
// BUILD
//////////////////////////////////////////////////////////////////////

Task("Build")
	.IsDependentOn("NuGetRestore")
	.Does(() =>
	{
		// Find MSBuild for Visual Studio 2017
		DirectoryPath vsLatest  = VSWhereLatest();
		FilePath msBuildPathX64 = (vsLatest==null) ? null
									: vsLatest.CombineWithFilePath("./MSBuild/15.0/Bin/MSBuild.exe");

		Information("Building using MSBuild at " + msBuildPathX64);
		Information("Configuration is:"+configuration);

		MSBuild(RUNSETTINGS_SOLUTION, new MSBuildSettings
		{
			Configuration = configuration,
			ToolPath = msBuildPathX64,
			ToolVersion = MSBuildToolVersion.VS2017,
			EnvironmentVariables = new Dictionary<string, string>
			{
				["PackageVersion"] = packageVersion
			}
		});
	});


//////////////////////////////////////////////////////////////////////
// PACKAGE
//////////////////////////////////////////////////////////////////////

Task("CreatePackageDir")
	.Does(() =>
	{
		CreateDirectory(PACKAGE_DIR);
	});

Task("CreateWorkingImage")
	.IsDependentOn("CreatePackageDir")
	.Does(() =>
	{
		CreateDirectory(PACKAGE_IMAGE_DIR);
		CleanDirectory(PACKAGE_IMAGE_DIR);

		CopyFileToDirectory("LICENSE.txt", PACKAGE_IMAGE_DIR);

		// dotnet publish doesn't work for .NET 3.5
		var net35Files = new FilePath[]
		{
			ADAPTER_BIN_DIR_NET35 + "NUnit3.TestAdapter.dll",
			ADAPTER_BIN_DIR_NET35 + "NUnit3.TestAdapter.pdb",
			ADAPTER_BIN_DIR_NET35 + "nunit.engine.dll",
			ADAPTER_BIN_DIR_NET35 + "nunit.engine.api.dll"
		};

		var net35Dir = PACKAGE_IMAGE_DIR + "build/net35";
		CreateDirectory(net35Dir);
		CopyFiles(net35Files, net35Dir);
		CopyFileToDirectory("nuget/net35/NUnit3TestAdapter.props", net35Dir);

		var netcoreDir = PACKAGE_IMAGE_DIR + "build/netcoreapp1.0";
		DotNetCorePublish(ADAPTER_PROJECT, new DotNetCorePublishSettings
		{
			Configuration = configuration,
			OutputDirectory = netcoreDir,
			Framework = "netcoreapp1.0"
		});
		CopyFileToDirectory("nuget/netcoreapp1.0/NUnit3TestAdapter.props", netcoreDir);
	});

Task("PackageZip")
	.IsDependentOn("CreateWorkingImage")
	.Does(() =>
	{
		Zip(PACKAGE_IMAGE_DIR, File(PACKAGE_DIR + packageName + ".zip"));
	});

Task("PackageNuGet")
	.IsDependentOn("CreateWorkingImage")
	.Does(() =>
	{
		NuGetPack("nuget/NUnit3TestAdapter.nuspec", new NuGetPackSettings()
		{
			Version = packageVersion,
			BasePath = PACKAGE_IMAGE_DIR,
			OutputDirectory = PACKAGE_DIR
		});
	});

Task("PackageVsix")
	.IsDependentOn("CreatePackageDir")
	.Does(() =>
	{
		CopyFile(
			BIN_DIR + "NUnit3TestAdapter.vsix",
			PACKAGE_DIR + packageName + ".vsix");
	});

Task("BuildAllTemplate")
	.Does(() => 
	{
		CopyToArchive("AllTemplate/","AllRunSettings.runsettings","AllRunSettings.zip");
	});


Task("CoverageNoParallelTemplate")
	.Does(() => 
	{
		CopyToArchive("CoverageNoParallelTemplate/","CoverageNoParallel.runsettings","coveragenoparallel.zip");
	});

Task("ParallelTemplate")
	.Does(() => 
	{
		CopyToArchive("ParallelTemplate/","parallel.runsettings","parallel.zip");
	});

Task("PackageIt")
.Does(()=>
{
   CopyFileToDirectory(ALLTEMPLATE+"AllRunSettings.zip", ITEMTEMPLATESFOLDER);
   CopyFileToDirectory(COVERAGENOPARALLEL+"coveragenoparallel.zip", ITEMTEMPLATESFOLDER);
   CopyFileToDirectory(PARALLEL+"Parallel.zip", ITEMTEMPLATESFOLDER);
});


//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Rebuild")
	.IsDependentOn("Clean")
	.IsDependentOn("Build");


Task("Default")
	.IsDependentOn("BuildAllTemplate")
	.IsDependentOn("CoverageNoParallelTemplate")
	.IsDependentOn("ParallelTemplate")
	.IsDependentOn("PackageIt")
	.IsDependentOn("Build")
	;

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
