using Albatross.CodeAnalysis;
using Albatross.CodeGen.CommandLine.Parameters;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	[Obsolete]
	public class LegacyCSharpWebClientCodeGenCommandHandler : IAsyncCommandHandler {
		private readonly CodeGenParams parameters;
		private readonly LegacyCreateHttpClientRegistrations legacyCreateHttpClientRegistrations;
		private readonly CSharpWebClientSettings settings;
		private readonly ILogger<LegacyCSharpWebClientCodeGenCommandHandler> logger;
		private readonly ConvertApiControllerToControllerModel convertToWebApi;
		private readonly LegacyConvertWebApiToCSharpFile converToCSharpCodeStack;

		public LegacyCSharpWebClientCodeGenCommandHandler(
			LegacyCreateHttpClientRegistrations legacyCreateHttpClientRegistrations,
			CodeGenParams parameters,
			ILogger<LegacyCSharpWebClientCodeGenCommandHandler> logger,
			ConvertApiControllerToControllerModel convertToWebApi,
			LegacyConvertWebApiToCSharpFile converToCSharpFile) {
			this.parameters = parameters;
			this.settings = parameters.CodeGenSettings as CSharpWebClientSettings ?? new CSharpWebClientSettings();
			this.legacyCreateHttpClientRegistrations = legacyCreateHttpClientRegistrations;
			this.logger = logger;
			this.convertToWebApi = convertToWebApi;
			this.converToCSharpCodeStack = converToCSharpFile;
		}

		public Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var controllerClass = new List<INamedTypeSymbol>();
			foreach (var syntaxTree in parameters.Compilation.SyntaxTrees) {
				var semanticModel = parameters.Compilation.GetSemanticModel(syntaxTree);
				var dtoClassWalker = new ApiControllerClassWalker(semanticModel, settings.ControllerFilters());
				dtoClassWalker.Visit(syntaxTree.GetRoot());
				controllerClass.AddRange(dtoClassWalker.Result);
			}
			var models = new List<ControllerInfo>();
			foreach (var controller in controllerClass) {
				if (string.IsNullOrEmpty(parameters.AdhocFilter) || controller.GetFullName().Contains(parameters.AdhocFilter, System.StringComparison.InvariantCultureIgnoreCase)) {
					logger.LogInformation("Generating proxy for {controller}", controller.Name);
					var webApi = this.convertToWebApi.Convert(controller);
					webApi.ApplyMethodFilters(settings.ControllerMethodFilters());
					models.Add(webApi);
					var csharpFile = this.converToCSharpCodeStack.Convert(webApi);
					if (parameters.OutputDirectory != null) {
						using (var streamWriter = new System.IO.StreamWriter(System.IO.Path.Join(parameters.OutputDirectory.FullName, csharpFile.FileName))) {
							csharpFile.Generate(streamWriter);
						}
					} else {
						csharpFile.Generate(System.Console.Out);
					}
				}
			}
			BuildRegistrationMethod(models);
			return Task.FromResult(0);
		}

		void BuildRegistrationMethod(IEnumerable<ControllerInfo> models) {
			var file = this.legacyCreateHttpClientRegistrations.Generate(models);
			if (parameters.OutputDirectory != null) {
				using (var streamWriter = new System.IO.StreamWriter(System.IO.Path.Join(parameters.OutputDirectory.FullName, file.FileName))) {
					streamWriter.Code(file);
				}
			} else {
				System.Console.Out.Code(file);
			}
		}
	}
}