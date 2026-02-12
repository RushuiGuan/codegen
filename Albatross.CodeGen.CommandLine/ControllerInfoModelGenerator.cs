using Albatross.CodeAnalysis;
using Albatross.CodeGen.CommandLine.Parameters;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	public class ControllerInfoModelGenerator : IAsyncCommandHandler {
		private readonly ConvertApiControllerToControllerModel converter;
		private readonly CodeGenParams parameters;
		private readonly Compilation compilation;
		private readonly CodeGenSettings settings;

		public ControllerInfoModelGenerator(ConvertApiControllerToControllerModel converter, CodeGenParams parameter, ICompilationFactory compilationFactory, ICodeGenSettingsFactory settingsFactory) {
			this.converter = converter;
			this.parameters = parameter;
			this.compilation = this.parameters.Compilation;
			this.settings = this.parameters.CodeGenSettings ?? new CodeGenSettings();
		}

		public Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var controllerClass = new List<INamedTypeSymbol>();
			foreach (var syntaxTree in compilation.SyntaxTrees) {
				var semanticModel = compilation.GetSemanticModel(syntaxTree);
				var classWalker = new ApiControllerClassWalker(semanticModel, settings.ControllerFilters());
				classWalker.Visit(syntaxTree.GetRoot());
				controllerClass.AddRange(classWalker.Result);
			}
			foreach (var item in controllerClass) {
				if (string.IsNullOrEmpty(parameters.AdhocFilter) || item.GetFullName().Contains(parameters.AdhocFilter, System.StringComparison.InvariantCultureIgnoreCase)) {
					var model = converter.Convert(item);
					model.ApplyMethodFilters(settings.ControllerMethodFilters());
					var text = JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, });
					if (parameters.OutputDirectory != null) {
						using (var streamWriter = new System.IO.StreamWriter(System.IO.Path.Join(parameters.OutputDirectory.FullName, $"{item.Name}.json"))) {
							streamWriter.BaseStream.SetLength(0);
							streamWriter.Write(text);
						}
					} else {
						System.Console.Out.WriteLine(text);
					}
				}
			}
			return Task.FromResult(0);
		}
	}
}