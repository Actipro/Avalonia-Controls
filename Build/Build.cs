using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Tools.NuGet;
using Serilog;
using System;
using System.Linq;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;

namespace ActiproSoftware.Tools.Builds {

	public partial class Build : NukeBuild {

		#region Parameters

		[Parameter("Configuration to build ('Debug' or 'Release')")]
		readonly Configuration Configuration = Configuration.Debug;

		#endregion

		#region Solutions

		[Solution("Samples/SampleBrowser/SampleBrowser.Desktop.sln")]
		readonly Solution SampleBrowserDesktopSolution;
		
		[Solution("Samples/SampleBrowser/SampleBrowser.Web.sln")]
		readonly Solution SampleBrowserWebSolution;

		[Solution("Source/Avalonia-Libraries.sln")]
		readonly Solution SourceLibrariesSolution;

		Solution[] SampleSolutions => [SampleBrowserDesktopSolution, SampleBrowserWebSolution];  // MSBuild

		Solution[] SourceSolutions => [SourceLibrariesSolution];  // MSBuild

		#endregion

		#region Core Build

		public Build() {
			NoLogo = true;
		}

		static int Main() {
			return Execute<Build>(b => b.IntegrationBuild);
		}

		#endregion

		#region Primary Targets

		Target IntegrationBuild => _ => _
			.DependsOn(Compile)
			.Executes();

		#endregion

		#region Compile Targets

		Target CompileSampleProjects => _ => _
			.Unlisted()
			.Executes(() => {

				foreach (var solution in SampleSolutions) {
					MSBuild(_ => _
						.SetSolutionFile(solution)
						.SetRestore(true)
						.SetConfiguration(Configuration)
						.SetVerbosity(MSBuildVerbosity.Minimal)
						.SetMaxCpuCount(Environment.ProcessorCount)
						.SetProperty("BuildInParallel", "true")
					);
					Log.Debug(string.Empty);
				}

			});

		Target CompileSourceProjects => _ => _
			.Unlisted()
			.Executes(() => {

				foreach (var solution in SourceSolutions) {
					MSBuild(_ => _
						.SetSolutionFile(solution)
						.SetRestore(true)
						.SetConfiguration(Configuration)
						.SetVerbosity(MSBuildVerbosity.Minimal)
						.SetMaxCpuCount(Environment.ProcessorCount)
						.SetProperty("BuildInParallel", "true")
					);
					Log.Debug(string.Empty);
				}

			});

		Target Compile => _ => _
			.Unlisted()
			.DependsOn(CompileSourceProjects, CompileSampleProjects)
			.Executes();

		#endregion

	}

}
