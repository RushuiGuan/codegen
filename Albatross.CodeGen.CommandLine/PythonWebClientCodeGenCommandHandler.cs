using Albatross.CodeAnalysis;
using Albatross.CodeGen.CommandLine.Parameters;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Python;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.CommandLine;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	public class PythonWebClientCodeGenCommandHandler : BaseHandler<CodeGenParams> {
		private readonly ILogger<PythonWebClientCodeGenCommandHandler> logger;
		private readonly Compilation compilation;
		private readonly CodeGenSettings settings;
		private readonly ConvertApiControllerToControllerModel convertToWebApi;
		private readonly ConvertControllerModelToPythonFile converToPythonFile;

		public PythonWebClientCodeGenCommandHandler(ParseResult result, CodeGenParams parameters,
			ILogger<PythonWebClientCodeGenCommandHandler> logger,
			ConvertApiControllerToControllerModel convertToWebApi,
			ConvertControllerModelToPythonFile converToPythonFile) :base(result,parameters){
			this.logger = logger;
			this.compilation = parameters.Compilation;
			this.settings = parameters.CodeGenSettings ?? new PythonWebClientSettings();
			this.convertToWebApi = convertToWebApi;
			this.converToPythonFile = converToPythonFile;
		}

		public override Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var models = new List<INamedTypeSymbol>();
			foreach (var syntaxTree in compilation.SyntaxTrees) {
				var semanticModel = compilation.GetSemanticModel(syntaxTree);
				var dtoClassWalker = new ApiControllerClassWalker(semanticModel, settings.ControllerFilters());
				dtoClassWalker.Visit(syntaxTree.GetRoot());
				models.AddRange(dtoClassWalker.Result);
			}
			var files = new List<PythonFileDeclaration>();
			foreach (var model in models) {
				if (string.IsNullOrEmpty(parameters.AdhocFilter) || model.GetFullName().Contains(parameters.AdhocFilter)) {
					logger.LogInformation("Generating proxy for {controller}", model.Name);
					var webApi = this.convertToWebApi.Convert(model);
					webApi.ApplyMethodFilters(settings.ControllerMethodFilters());
					var file = this.converToPythonFile.Convert(webApi);
					file.Generate(System.Console.Out);
					files.Add(file);
					logger.LogInformation("directory: {data}", parameters.OutputDirectory?.FullName);
					if (parameters.OutputDirectory != null) {
						using (var writer = new System.IO.StreamWriter(System.IO.Path.Join(parameters.OutputDirectory.FullName, file.FileName))) {
							file.Generate(writer);
						}
					}
				}
			}
			return Task.FromResult(0);
		}
	}
}