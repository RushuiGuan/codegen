using Albatross.CodeAnalysis;
using Albatross.CodeGen.CommandLine.Parameters;
using Albatross.CodeGen.TypeScript.Declarations;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CodeGen.WebClient.TypeScript;
using Albatross.CommandLine;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	public class TypeScriptDtoCodeGenCommandHandler : BaseHandler<CodeGenParams> {
		private readonly Compilation compilation;
		private readonly CodeGenSettings settings;
		private readonly ConvertClassSymbolToDtoClassModel dto2Model;
		private readonly ConvertEnumSymbolToDtoEnumModel enum2Model;
		private readonly ConvertDtoClassModelToTypeScriptInterface dtoModel2TypeScript;
		private readonly ConvertEnumModelToTypeScriptEnum enumModel2TypeScript;

		public TypeScriptDtoCodeGenCommandHandler(ParseResult result, Compilation compilation,
			CodeGenSettings settings,
			ConvertClassSymbolToDtoClassModel dto2Model,
			ConvertEnumSymbolToDtoEnumModel enum2Model,
			ConvertDtoClassModelToTypeScriptInterface dtoModel2TypeScript,
			ConvertEnumModelToTypeScriptEnum enumModel2TypeScript,
			CodeGenParams parameters) : base(result, parameters) {
			this.compilation = compilation;
			this.settings = settings;
			this.dto2Model = dto2Model;
			this.enum2Model = enum2Model;
			this.dtoModel2TypeScript = dtoModel2TypeScript;
			this.enumModel2TypeScript = enumModel2TypeScript;
		}

		public override Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var dtoModels = new List<DtoClassInfo>();
			var enumModels = new List<EnumInfo>();
			foreach (var syntaxTree in compilation.SyntaxTrees) {
				var semanticModel = compilation.GetSemanticModel(syntaxTree);
				var symbolWalker = new DtoClassEnumWalker(semanticModel, settings.DtoFilters());
				symbolWalker.Visit(syntaxTree.GetRoot());
				dtoModels.AddRange(symbolWalker.DtoClasses
					.Where(x => string.IsNullOrEmpty(parameters.AdhocFilter) || x.GetFullName().Contains(parameters.AdhocFilter, System.StringComparison.InvariantCultureIgnoreCase))
					.Select(x => dto2Model.Convert(x)));

				enumModels.AddRange(symbolWalker.EnumTypes
					.Where(x => string.IsNullOrEmpty(parameters.AdhocFilter) || x.GetFullName().Contains(parameters.AdhocFilter, System.StringComparison.InvariantCultureIgnoreCase))
					.Select(x => enum2Model.Convert(x)));
			}
			var dtoFile = new TypeScriptFileDeclaration("dto.generated") {
				EnumDeclarations = enumModels.Select(x => enumModel2TypeScript.Convert(x)),
				InterfaceDeclarations = dtoModels
					.Select(x => dtoModel2TypeScript.Convert(x)).ToList(),
			};
			dtoFile.Generate(System.Console.Out);
			if (parameters.OutputDirectory != null) {
				using (var writer = new StreamWriter(Path.Join(parameters.OutputDirectory.FullName, dtoFile.FileName))) {
					dtoFile.Generate(writer);
				}
			}
			return Task.FromResult(0);
		}
	}
}