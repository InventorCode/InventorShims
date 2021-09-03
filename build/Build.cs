using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DocFX;
using Nuke.Common.Tools.DotCover;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Tools.NuGet;
using Nuke.Common.Tools.ReportGenerator;
using Nuke.Common.Utilities.Collections;
using Nuke.GitHub;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Nuke.Common.ChangeLog.ChangelogTasks;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DocFX.DocFXTasks;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;
using static Nuke.Common.Tools.NuGet.NuGetTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.GitHub.GitHubTasks;


[CheckBuildProjectConfigurations]
internal class Build : NukeBuild
{
    // Console application entry. Also defines the default target.
    public static int Main() => Execute<Build>(x => x.Compile);

    //[GitVersion] private readonly GitVersion GitVersion;
    // Semantic versioning. Must have 'GitVersion.CommandLine' referenced.

    [GitRepository] private readonly GitRepository GitRepository;
    // Parses origin, branch name and head from git config.

    [Parameter] private string MyGetSource = "https://api.nuget.org/v3/index.json";
    [Parameter] private string MyGetApiKey;
    [Parameter] private string GitHubAuthenticationToken;

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    private readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] private readonly Solution Solution;
    [GitVersion(Framework = "netcoreapp3.1")] private readonly GitVersion GitVersion;

    [PackageExecutable(
    packageId: "GitVersion.CommandLine",
    packageExecutable: "gitversion.exe")]
    private readonly Tool gVersion;

    private AbsolutePath SourceDirectory => RootDirectory / "src";
    private AbsolutePath ProjectPath => RootDirectory / "src" / "InventorShims"/"InventorShims.csproj";
    private AbsolutePath TestsDirectory => RootDirectory / "tests";
    private AbsolutePath OutputDirectory => RootDirectory / "artifacts";

    private Target Clean => _ => _
         .Before(Restore)
         .Executes(() =>
         {
             SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
             TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
             EnsureCleanDirectory(OutputDirectory);
         });

    private Target Restore => _ => _
         .Executes(() =>
         {
             MSBuild(s => s
                 .SetTargetPath(Solution)
                 .SetTargets("Restore"));
         });

    private Target Compile => _ => _
         .DependsOn(Restore)
         .Executes(() =>
         {
             gVersion($"/UpdateAssemblyInfo");

             MSBuild(s => s
                 .SetTargetPath(Solution)
                 .SetTargets("Rebuild")
                 .SetConfiguration(Configuration)
                 .SetAssemblyVersion(GitVersion.AssemblySemVer)
                 .SetFileVersion(GitVersion.AssemblySemFileVer)
                 .SetInformationalVersion(GitVersion.InformationalVersion)
                 .SetMaxCpuCount(Environment.ProcessorCount)
                 .SetNodeReuse(IsLocalBuild));
         });

    private string NuGetReleaseNotes => GetNuGetReleaseNotes(RootDirectory / "CHANGELOG.md");

    private Target Pack => _ => _
     .DependsOn(Compile)
     .Executes(() =>
     {
         DotNetPack(s => s
             .SetProject(ProjectPath)
             .SetVersion(GitVersion.NuGetVersionV2)
             .SetPackageReleaseNotes(GetNuGetReleaseNotes(ChangeLogFile, GitRepository))
             .SetOutputDirectory(OutputDirectory)
             ); ;
     });

    private Target Push => _ => _
     .DependsOn(Pack)
     .Requires(() => MyGetSource)
     .Requires(() => MyGetApiKey)
     .Executes(() =>
     {
         GlobFiles(OutputDirectory, "*.nupkg").NotEmpty()
             .Where(x => !x.EndsWith("symbols.nupkg"))
             .ForEach(x =>
             {
                 NuGetPush(s => s
                     .SetTargetPath(x)
                     .SetSource(MyGetSource)
                     .SetApiKey(MyGetApiKey)
                     );
             });
     });

    private Target Release => _ => _
     .DependsOn(Compile)
     .Executes(() =>
     {
         //gVersion($"/UpdateAssemblyInfo");

         //MSBuild(s => s
         //    .SetTargetPath(Solution)
         //    .SetTargets("Rebuild")
         //    .SetConfiguration("Debug")
         //    .SetAssemblyVersion(GitVersion.AssemblySemVer)
         //    .SetFileVersion(GitVersion.AssemblySemFileVer)
         //    .SetInformationalVersion(GitVersion.InformationalVersion)
         //    .SetMaxCpuCount(Environment.ProcessorCount)
         //    .SetNodeReuse(IsLocalBuild));
     });

    private Target PublishGitHubRelease => _ => _
     .DependsOn(Pack)
     .Requires(() => GitHubAuthenticationToken)
     //.OnlyWhen( GitVersion.BranchName.Equals("master") || GitVersion.BranchName.Equals("origin/master"))
     .Executes<Task>(async () =>
     {
         var releaseTag = $"v{GitVersion.MajorMinorPatch}";

         var changeLogSectionEntries = ExtractChangelogSectionNotes(ChangeLogFile);
         var latestChangeLog = changeLogSectionEntries
             .Aggregate((c, n) => c + Environment.NewLine + n);
         var completeChangeLog = $"## {releaseTag}" + Environment.NewLine + latestChangeLog;

         var repositoryInfo = GetGitHubRepositoryInfo(GitRepository);
         var nuGetPackages = GlobFiles(OutputDirectory, "*.nupkg").NotEmpty().ToArray();

         await PublishRelease(s => s
             .SetArtifactPaths(nuGetPackages)
             .SetCommitSha(GitVersion.Sha)
             .SetReleaseNotes(completeChangeLog)
             .SetRepositoryName(repositoryInfo.repositoryName)
             .SetRepositoryOwner(repositoryInfo.gitHubOwner)
             .SetTag(releaseTag)
             .SetToken(GitHubAuthenticationToken));
     });

    private string DocFxFile => RootDirectory / "docfx" / "docfx.json";
    private string DocFxOutputFolder => RootDirectory / "doc";
    private string DocFxIntermediateFolder => TemporaryDirectory / "docfx";
    private string DocFXTemplateFolder => RootDirectory / "docfx" / "templates" / "inventor-shims";
    string ChangeLogFile => RootDirectory / "CHANGELOG.md";
    private string ChangeLogDestination => Path.Join(RootDirectory, "docfx", "articles", "CHANGELOG.md");

    private Target BuildDocumentation => _ => _
    .DependsOn(Clean)
    .DependsOn(Restore)
         .Executes(() =>
         {
             //Update CHANGELOG.md
             if (File.Exists(ChangeLogDestination))
             {
                 File.Delete(ChangeLogDestination);
             }
             File.Copy(ChangeLogFile, ChangeLogDestination);

             //Build documentation
             DocFXBuild(s => s
         .SetConfigFile(DocFxFile)
         .EnableForceRebuild()
         .SetOutputFolder(DocFxOutputFolder)
         .EnableCleanupCacheHistory()
         .SetIntermediateFolder(DocFxIntermediateFolder)
         .AddTemplates(DocFXTemplateFolder)
         );
         });
}