using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DocFX;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Utilities.Collections;
using System;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DocFX.DocFXTasks;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;
using static Nuke.Common.Tools.NuGet.NuGetTasks;

[CheckBuildProjectConfigurations]
internal class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    private readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] private readonly Solution Solution;
    [GitRepository] private readonly GitRepository GitRepository;
    [GitVersion(Framework = "netcoreapp3.1")] private readonly GitVersion GitVersion;

    [PackageExecutable(
    packageId: "GitVersion.CommandLine",
    packageExecutable: "gitversion.exe")]
    private readonly Tool gVersion;

    private AbsolutePath SourceDirectory => RootDirectory / "src";
    private AbsolutePath TestsDirectory => RootDirectory / "tests";
    private AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    private Target Clean => _ => _
         .Before(Restore)
         .Executes(() =>
         {
             SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
             TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
             EnsureCleanDirectory(ArtifactsDirectory);
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

    private Target Release => _ => _
     .DependsOn(Restore)
     .Executes(() =>
     {
         gVersion($"/UpdateAssemblyInfo");

         MSBuild(s => s
             .SetTargetPath(Solution)
             .SetTargets("Rebuild")
             .SetConfiguration("Debug")
             .SetAssemblyVersion(GitVersion.AssemblySemVer)
             .SetFileVersion(GitVersion.AssemblySemFileVer)
             .SetInformationalVersion(GitVersion.InformationalVersion)
             .SetMaxCpuCount(Environment.ProcessorCount)
             .SetNodeReuse(IsLocalBuild));
     });

    private string DocFxFile => RootDirectory / "docfx" / "docfx.json";
    private string DocFxOutputFolder => RootDirectory / "doc";
    private string DocFxIntermediateFolder => TemporaryDirectory / "docfx";
    private string DocFXTemplateFolder => RootDirectory / "docfx" / "templates" / "inventor-shims";

    private Target Docs => _ => _
         .DependsOn(Clean)
         .Before(Release)
         .Executes(() =>
         {
             DocFXBuild(s => s
             .SetConfigFile(DocFxFile)
             .EnableForceRebuild()
             .SetOutputFolder(DocFxOutputFolder)
             .EnableCleanupCacheHistory()
             .SetIntermediateFolder(DocFxIntermediateFolder)
             .AddTemplates(DocFXTemplateFolder)
             );
         });

    private Target Pack => _ => _
     .DependsOn(Release)
     .Executes(() =>
     {
         NuGetPack(s => s);
     });
}