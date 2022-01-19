using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
//using Nuke.Common.Tools.DocFX;
using Nuke.Common.Tools.DotCover;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Tools.NuGet;
using Nuke.Common.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
//using static Nuke.Common.Tools.DocFX.DocFXTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;

[CheckBuildProjectConfigurations]
internal class Build : NukeBuild
{
    // Console application entry. Also defines the default target.
    public static int Main() => Execute<Build>(x => x.Compile);

    //    [GitVersion] private readonly GitVersion GitVersion;
    // Semantic versioning. Must have 'GitVersion.CommandLine' referenced.

    [GitRepository] private readonly GitRepository GitRepository;
    // Parses origin, branch name and head from git config.

    [Parameter] private string MyGetSource = "https://api.nuget.org/v3/index.json";
    [Parameter] private string MyGetApiKey;
    [Parameter] private string GitHubAuthenticationToken;

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    private readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] private readonly Solution Solution;

    [GitVersion(Framework = "net5.0")]
    private readonly GitVersion GitVersion;

    private AbsolutePath SourceDirectory => RootDirectory / "src";
    private AbsolutePath ProjectPath => RootDirectory / "src" / "InventorShims" / "InventorShims.csproj";
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
                 MSBuild(s => s
                       .SetTargetPath(Solution)
                       .SetTargets("Rebuild")
                       .SetConfiguration("Release")
                       .SetAssemblyVersion(GitVersion.AssemblySemVer)
                       .SetFileVersion(GitVersion.AssemblySemFileVer)
                       .SetInformationalVersion(GitVersion.InformationalVersion)
                       .SetMaxCpuCount(Environment.ProcessorCount)
                       .SetNodeReuse(IsLocalBuild));
             });

    private Target Pack => _ => _
         .DependsOn(Compile)
         .Executes(() =>
         {
             DotNetPack(s => s
                 .SetProject(ProjectPath)
                 .SetVersion(GitVersion.NuGetVersionV2)
                 .SetOutputDirectory(OutputDirectory)
                 ); ;
         });

    private IReadOnlyCollection<AbsolutePath> Packages => OutputDirectory.GlobFiles("*.nupkg");

    private Target Push => _ => _
         .DependsOn(Pack)
         .Consumes(Pack)
         .Requires(() => MyGetSource)
         .Requires(() => MyGetApiKey)
         .Executes(() =>
         {
             DotNetNuGetPush(s => s
                .SetSource(MyGetSource)
                .SetApiKey(MyGetApiKey)
                .CombineWith(Packages, (_, file) => _
                .SetTargetPath(file)));
         });

    //private Target PublishGitHubRelease => _ => _
    // .DependsOn(Pack)
    // .Requires(() => GitHubAuthenticationToken)
    // .OnlyWhenStatic(() => GitRepository.IsOnMasterBranch() || GitRepository.IsOnReleaseBranch())
    // .Executes<Task>(async () =>
    // {
    //     var releaseTag = $"v{GitVersion.MajorMinorPatch}";

    //     var changeLogSectionEntries = ExtractChangelogSectionNotes(ChangeLogFile);
    //     var latestChangeLog = changeLogSectionEntries
    //         .Aggregate((c, n) => c + Environment.NewLine + n);
    //     var completeChangeLog = $"## {releaseTag}" + Environment.NewLine + latestChangeLog;

    //     var repositoryInfo = GetGitHubRepositoryInfo(GitRepository);
    //     var nuGetPackages = GlobFiles(OutputDirectory, "*.nupkg").NotEmpty().ToArray();

    //     await PublishRelease(s => s
    //         .SetArtifactPaths(nuGetPackages)
    //         .SetCommitSha(GitVersion.Sha)
    //         .SetReleaseNotes(completeChangeLog)
    //         .SetRepositoryName(repositoryInfo.repositoryName)
    //         .SetRepositoryOwner(repositoryInfo.gitHubOwner)
    //         .SetTag(releaseTag)
    //         .SetToken(GitHubAuthenticationToken));
    // });

    private AbsolutePath DocFxRoot => RootDirectory / "docfx";
    private AbsolutePath DocFxOutputFolder => RootDirectory / "docs";
    private AbsolutePath DocFxIntermediateFolder => TemporaryDirectory / "docfx";
    private AbsolutePath DocFXTemplateFolder => RootDirectory / "docfx" / "templates" / "inventor-shims";
    private AbsolutePath ChangeLogFile => RootDirectory / "CHANGELOG.md";
    private string ChangeLogDestination => Path.Join(RootDirectory, "docfx", "articles", "CHANGELOG.md");
    private AbsolutePath ContributingSource => RootDirectory / "CONTRIBUTING.md";
    private string ContributingDestination => Path.Join(RootDirectory, "docfx", "articles", "Contributing.md");
    private AbsolutePath DocFxFile => DocFxRoot / "docfx.json";

    //private Target docs => _ => _
    //    .DependsOn(Clean)
    //    .DependsOn(Restore)
    //         .Executes(() =>
    //         {
    //            //Update CHANGELOG.md
    //            if (File.Exists(ChangeLogDestination))
    //                 File.Delete(ChangeLogDestination);
    //             File.Copy(ChangeLogFile, ChangeLogDestination);

    //            //Update CONTRIBUITNG.md
    //            if (File.Exists(ContributingDestination))
    //                 File.Delete(ContributingDestination);
    //             File.Copy(ContributingSource, ContributingDestination);

    //            //Build documentation
    //            DocFXBuild(s => s
    //           .SetConfigFile(RootDirectory / "docfx" / "docfx.json")
    //           .SetOutputFolder(DocFxOutputFolder)
    //           .EnableForceRebuild()
    //           .EnableCleanupCacheHistory()
    //           .SetIntermediateFolder(DocFxIntermediateFolder)
    //           //.AddTemplates(DocFXTemplateFolder)
    //           );
    //         });
}