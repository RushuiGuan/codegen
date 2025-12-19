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
	public interface IProjectCompilationFactory {
		Task<Compilation> Create();
	}
	public class ProjectCompilationFactory : IProjectCompilationFactory {
		private readonly string project;
		private readonly ILogger<ProjectCompilationFactory> logger;
		Compilation? compilation;
		SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

		public ProjectCompilationFactory(CodeGenCommandOptions projectOptions, ILogger<ProjectCompilationFactory> logger) {
			this.project = projectOptions.ProjectFile.FullName;
			this.logger = logger;
		}
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