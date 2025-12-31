using Albatross.CodeAnalysis;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	public class DtoClassInfoModelGenerator : BaseHandler<CodeGenParams> {
		private Compilation compilation;
		private ConvertClassSymbolToDtoClassModel dtoConverter;
		private readonly ConvertEnumSymbolToDtoEnumModel enumConverter;
		private CodeGenSettings settings;

		public DtoClassInfoModelGenerator(ParseResult result, Compilation compilation, ConvertClassSymbolToDtoClassModel dtoConverter,
			ConvertEnumSymbolToDtoEnumModel enumConverter,
			CodeGenSettings settings,
			CodeGenParams parameters) : base(result, parameters) {
			this.compilation = compilation;
			this.dtoConverter = dtoConverter;
			this.enumConverter = enumConverter;
			this.settings = settings;
		}

		public override Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var dtoClasses = new List<INamedTypeSymbol>();
			var enumClasses = new List<INamedTypeSymbol>();
			foreach (var syntaxTree in compilation.SyntaxTrees) {
				var semanticModel = compilation.GetSemanticModel(syntaxTree);
				var symbolWalker = new DtoClassEnumWalker(semanticModel, settings.DtoFilters());
				symbolWalker.Visit(syntaxTree.GetRoot());
				dtoClasses.AddRange(symbolWalker.DtoClasses);
				enumClasses.AddRange(symbolWalker.EnumTypes);
			}
			var serializationOptions = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase, };
			var dtoModels = new List<DtoClassInfo>();
			foreach (var item in dtoClasses) {
				if (string.IsNullOrEmpty(parameters.AdhocFilter) || item.GetFullName().Contains(parameters.AdhocFilter, System.StringComparison.InvariantCultureIgnoreCase)) {
					var model = dtoConverter.Convert(item);
					dtoModels.Add(model);
				}
			}
			if (dtoModels.Any()) {
				var text = JsonSerializer.Serialize(dtoModels, serializationOptions);
				this.Writer.WriteLine(text);
			}
			var enumModels = new List<EnumInfo>();
			foreach (var item in enumClasses) {
				if (string.IsNullOrEmpty(parameters.AdhocFilter) || item.GetFullName().Contains(parameters.AdhocFilter, System.StringComparison.InvariantCultureIgnoreCase)) {
					var model = enumConverter.Convert(item);
					enumModels.Add(model);
				}
			}
			if (enumModels.Any()) {
				var text = JsonSerializer.Serialize(enumModels, serializationOptions);
				this.Writer.WriteLine(text);
			}

			if (parameters.OutputDirectory != null) {
				if (dtoModels.Any()) {
					using (var stream = File.OpenWrite(Path.Join(parameters.OutputDirectory.FullName, "dto.json"))) {
						stream.SetLength(0);
						JsonSerializer.Serialize(stream, dtoModels, serializationOptions);
					}
				}
				if (enumModels.Any()) {
					using (var stream = File.OpenWrite(Path.Join(parameters.OutputDirectory.FullName, "enum.json"))) {
						stream.SetLength(0);
						JsonSerializer.Serialize(stream, enumModels, serializationOptions);
					}
				}
			}
			return Task.FromResult(0);
		}
	}
}