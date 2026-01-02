using Albatross.CodeAnalysis;
using Albatross.CodeGen.CommandLine.Parameters;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	public class DtoClassInfoModelGenerator : IAsyncCommandHandler {
		private ConvertClassSymbolToDtoClassModel dtoConverter;
		private readonly ConvertEnumSymbolToDtoEnumModel enumConverter;
		private readonly CodeGenParams parameters;

		public DtoClassInfoModelGenerator(ConvertClassSymbolToDtoClassModel dtoConverter,
			ConvertEnumSymbolToDtoEnumModel enumConverter,
			CodeGenParams parameters) {
			this.dtoConverter = dtoConverter;
			this.enumConverter = enumConverter;
			this.parameters = parameters;
		}

		public Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var dtoClasses = new List<INamedTypeSymbol>();
			var enumClasses = new List<INamedTypeSymbol>();
			foreach (var syntaxTree in parameters.Compilation.SyntaxTrees) {
				var semanticModel = parameters.Compilation.GetSemanticModel(syntaxTree);
				var symbolWalker = new DtoClassEnumWalker(semanticModel, parameters.CodeGenSettings?.DtoFilters() ?? []);
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
				Console.Out.WriteLine(text);
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
				Console.Out.WriteLine(text);
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