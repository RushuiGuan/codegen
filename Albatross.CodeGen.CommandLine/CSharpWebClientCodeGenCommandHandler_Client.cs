using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	public class CSharpWebClientCodeGenCommandHandler_Client : BaseHandler<CodeGenCommandOptions> {
		private readonly CreateHttpClientRegistrations2 createHttpClientRegistrations;
		private readonly Compilation compilation;
		private readonly CSharpWebClientSettings settings;
		private readonly ILogger<CSharpWebClientCodeGenCommandHandler_Client> logger;
		private readonly ConvertApiControllerToControllerModel convertToWebApi;
		private readonly ConvertWebApiToCSharpFile converToCSharpCodeStack;

		public CSharpWebClientCodeGenCommandHandler_Client(IOptions<CodeGenCommandOptions> options,
			CreateHttpClientRegistrations2 createHttpClientRegistrations,
			Compilation compilation,
			CSharpWebClientSettings settings,
			ILogger<CSharpWebClientCodeGenCommandHandler_Client> logger,
			ConvertApiControllerToControllerModel convertToWebApi,
			ConvertWebApiToCSharpFile converToCSharpFile) : base(options) {
			this.createHttpClientRegistrations = createHttpClientRegistrations;
			this.compilation = compilation;
			this.settings = settings;
			this.logger = logger;
			this.convertToWebApi = convertToWebApi;
			this.converToCSharpCodeStack = converToCSharpFile;
		}

		public override Task<int> InvokeAsync(InvocationContext context) {
			var controllerClass = new List<INamedTypeSymbol>();
			foreach (var syntaxTree in compilation.SyntaxTrees) {
				var semanticModel = compilation.GetSemanticModel(syntaxTree);
				var dtoClassWalker = new ApiControllerClassWalker(semanticModel, settings.ControllerFilters());
				dtoClassWalker.Visit(syntaxTree.GetRoot());
				controllerClass.AddRange(dtoClassWalker.Result);
			}
			var models = new List<ControllerInfo>();
			foreach (var controller in controllerClass) {
				if (string.IsNullOrEmpty(options.AdhocFilter) || controller.GetFullName().Contains(options.AdhocFilter, System.StringComparison.InvariantCultureIgnoreCase)) {
					logger.LogInformation("Generating proxy for {controller}", controller.Name);
					var webApi = this.convertToWebApi.Convert(controller);
					webApi.ApplyMethodFilters(settings.ControllerMethodFilters());
					models.Add(webApi);
					var csharpFile = this.converToCSharpCodeStack.Convert(webApi);
					if (options.OutputDirectory != null) {
						using (var streamWriter = new System.IO.StreamWriter(System.IO.Path.Join(options.OutputDirectory.FullName, csharpFile.FileName))) {
							csharpFile.Generate(streamWriter);
						}
					} else {
						csharpFile.Generate(this.writer);
					}
				}
			}
			BuildRegistrationMethod(models);
			return Task.FromResult(0);
		}

		void BuildRegistrationMethod(IEnumerable<ControllerInfo> models) {
			var file = this.createHttpClientRegistrations.Generate(models);
			if (options.OutputDirectory != null) {
				using (var streamWriter = new System.IO.StreamWriter(System.IO.Path.Join(options.OutputDirectory.FullName, file.FileName))) {
					streamWriter.Code(file);
				}
			} else {
				this.writer.Code(file);
			}
		}
	}
}