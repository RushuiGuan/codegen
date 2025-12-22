using Albatross.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	/// <summary>
	/// Interface for creating Roslyn compilation objects from project files
	/// </summary>
	public interface IProjectCompilationFactory {
		/// <summary>
		/// Creates a compilation object from the configured project
		/// </summary>
		/// <returns>A task that resolves to a Roslyn Compilation instance</returns>
		Task<Compilation> Create();
	}
	/// <summary>
	/// Factory for creating Roslyn compilation objects from MSBuild project files with caching support
	/// </summary>
	public class ProjectCompilationFactory : IProjectCompilationFactory {
		private readonly string project;
		private readonly ILogger<ProjectCompilationFactory> logger;
		Compilation? compilation;
		SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

		/// <summary>
		/// Initializes a new instance of the ProjectCompilationFactory class
		/// </summary>
		/// <param name="projectOptions">Command options containing project file path</param>
		/// <param name="logger">Logger for diagnostic messages</param>
		public ProjectCompilationFactory(CodeGenCommandOptions projectOptions, ILogger<ProjectCompilationFactory> logger) {
			this.project = projectOptions.ProjectFile.FullName;
			this.logger = logger;
		}
		/// <summary>
		/// Creates a Roslyn compilation from the project file, including source generators if present
		/// </summary>
		/// <returns>A task that resolves to the compiled project with all source generators applied</returns>
		/// <exception cref="InvalidOperationException">Thrown when compilation cannot be created from the project</exception>
		public async Task<Compilation> Create() {
			if (this.compilation == null) {
				using var asyncLock = new AsyncLock(this.semaphore);
				if (this.compilation == null) {
					var workspace = MSBuildWorkspace.Create();
					var project = await workspace.OpenProjectAsync(this.project);
					var parseOptions = (CSharpParseOptions)(project.ParseOptions ?? CSharpParseOptions.Default);
					this.compilation = await project.GetCompilationAsync();
					if (compilation == null) {
						throw new InvalidOperationException($"Unable to create compilation for project: {project.Name}");
					}
					var generators = project.AnalyzerReferences
						.Where(a=>a.Display != "Microsoft.CodeAnalysis.Razor.Compiler")
						.SelectMany(a => a.GetGenerators(LanguageNames.CSharp)).ToArray();

					if (generators.Length > 0) {
						var driver = CSharpGeneratorDriver.Create(generators).WithUpdatedParseOptions(parseOptions);
						driver.RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out var diagnostics);
						if (diagnostics.Length > 0) {
							foreach (var diag in diagnostics) {
								logger.LogError("Received warning while running generator: {content}", diag.ToString());
							}
						}
						this.compilation = updatedCompilation;
					}
				}
			}
			return this.compilation;
		}
	}
}