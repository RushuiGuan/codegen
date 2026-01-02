using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.CommandLine.Inputs;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.Extensions.Logging;
using System;
using System.CommandLine;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine.Parameters {
	[OptionHandler(typeof(LoadCSharpProject))]
	public class ProjectCompilationOption : InputFileOption, IUseContextValue {
		public ProjectCompilationOption() : this("--project-file", "-p") { }
		public ProjectCompilationOption(string name, params string[] aliases) : base(name, aliases) { }
	}
	public class LoadCSharpProject : IAsyncOptionHandler<ProjectCompilationOption> {
		private readonly ILogger<LoadCSharpProject> logger;
		private readonly ICommandContext context;

		public LoadCSharpProject(ILogger<LoadCSharpProject> logger, ICommandContext context) {
			this.logger = logger;
			this.context = context;
		}

		public async Task InvokeAsync(ProjectCompilationOption symbol, ParseResult result, CancellationToken cancellationToken) {
			var file = result.GetValue(symbol);
			if (file != null) {
				var workspace = MSBuildWorkspace.Create();
				var project = await workspace.OpenProjectAsync(file.FullName);
				var parseOptions = (CSharpParseOptions)(project.ParseOptions ?? CSharpParseOptions.Default);
				var compilation = await project.GetCompilationAsync();
				if (compilation == null) {
					throw new InvalidOperationException($"Unable to create compilation for project: {project.Name}");
				}
				var generators = project.AnalyzerReferences
					.Where(a => a.Display != "Microsoft.CodeAnalysis.Razor.Compiler")
					.SelectMany(a => a.GetGenerators(LanguageNames.CSharp)).ToArray();

				if (generators.Length > 0) {
					var driver = CSharpGeneratorDriver.Create(generators).WithUpdatedParseOptions(parseOptions);
					driver.RunGeneratorsAndUpdateCompilation(compilation, out var updatedCompilation, out var diagnostics);
					if (diagnostics.Length > 0) {
						foreach (var diag in diagnostics) {
							logger.LogError("Received warning while running generator: {content}", diag.ToString());
						}
					}
					this.context.SetValue(symbol.Name, compilation);
				}
			}
		}
	}
}
