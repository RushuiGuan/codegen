using Albatross.CodeAnalysis;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.CommandLine;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	public class CSharpWebClientCodeGenCommandHandler_Client : BaseHandler<CodeGenParams> {
		private readonly CreateHttpClientRegistrations createHttpClientRegistrations;
		private readonly Compilation compilation;
		private readonly CSharpWebClientSettings settings;
		private readonly ILogger<CSharpWebClientCodeGenCommandHandler_Client> logger;
		private readonly ConvertApiControllerToControllerModel convertToWebApi;
		private readonly ConvertWebApiToCSharpFile converToCSharpCodeStack;

		public CSharpWebClientCodeGenCommandHandler_Client(ParseResult result, CodeGenParams parameters,
			CreateHttpClientRegistrations createHttpClientRegistrations,
			Compilation compilation,
			CSharpWebClientSettings settings,
			ILogger<CSharpWebClientCodeGenCommandHandler_Client> logger,
			ConvertApiControllerToControllerModel convertToWebApi,
			ConvertWebApiToCSharpFile converToCSharpFile) : base(result, parameters) {
			this.createHttpClientRegistrations = createHttpClientRegistrations;
			this.compilation = compilation;
			this.settings = settings;
			this.logger = logger;
			this.convertToWebApi = convertToWebApi;
			this.converToCSharpCodeStack = converToCSharpFile;
		}

		public override Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var controllerClass = new List<INamedTypeSymbol>();
			foreach (var syntaxTree in compilation.SyntaxTrees) {
				var semanticModel = compilation.GetSemanticModel(syntaxTree);
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
						csharpFile.Generate(this.Writer);
					}
				}
			}
			BuildRegistrationMethod(models);
			return Task.FromResult(0);
		}

		void BuildRegistrationMethod(IEnumerable<ControllerInfo> models) {
			var file = this.createHttpClientRegistrations.Generate(models);
			if (parameters.OutputDirectory != null) {
				using (var streamWriter = new System.IO.StreamWriter(System.IO.Path.Join(parameters.OutputDirectory.FullName, file.FileName))) {
					streamWriter.Code(file);
				}
			} else {
				this.Writer.Code(file);
			}
		}
	}
}