using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Serilog;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Run);
    [Solution] readonly Solution Solution;
    Project MyProject => Solution.GetProject("nuke-testcontainers");

    Target Run => _ => _
        .Executes(() =>
        {
            Log.Information("**** Start image build ****");
            DotNetTasks.DotNetRun(c => c
                .SetProjectFile(MyProject));
        });
}