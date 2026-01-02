using Albatross.CodeAnalysis;
using Albatross.CodeGen.CommandLine.Parameters;
using Albatross.CodeGen.Python;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Python;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	public class PythonDtoCodeGenCommandHandler : BaseHandler<CodeGenParams> {
		private readonly Compilation compilation;
		private readonly CodeGenSettings settings;
		private readonly ConvertClassSymbolToDtoClassModel dto2Model;
		private readonly ConvertEnumSymbolToDtoEnumModel enum2Model;
		private readonly ConvertDtoClassModelToDataClass dtoModel2Python;
		private readonly ConvertEnumModelToPythonEnum enumModel2Python;

		public PythonDtoCodeGenCommandHandler(ParseResult result, Compilation compilation,
			CodeGenSettings settings,
			ConvertClassSymbolToDtoClassModel dto2Model,
			ConvertEnumSymbolToDtoEnumModel enum2Model,
			ConvertDtoClassModelToDataClass dtoModel2Python,
			ConvertEnumModelToPythonEnum enumModel2Python,
			CodeGenParams parameters) : base(result, parameters) {
			this.compilation = compilation;
			this.settings = settings;
			this.dto2Model = dto2Model;
			this.enum2Model = enum2Model;
			this.dtoModel2Python = dtoModel2Python;
			this.enumModel2Python = enumModel2Python;
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
			var dtoFile = new PythonFileDeclaration("dto") {
				Banner = [new CommentDeclaration("@generated"),],
				Imports = new ImportCollection([Defined.Identifiers.FutureAnnotations]).Imports,
				Classes = enumModels.Select(x => enumModel2Python.Convert(x))
					.Concat(dtoModels.Select(x => dtoModel2Python.Convert(x)))
					.ToList(),
			};
			dtoFile.Generate(System.Console.Out);
			if (parameters.OutputDirectory != null) {
				var file = Path.Join(parameters.OutputDirectory.FullName, dtoFile.FileName);
				using var stream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
				using var streamWriter = new StreamWriter(stream);
				dtoFile.Generate(streamWriter);
				streamWriter.Flush();
				stream.SetLength(stream.Position);
			}
			return Task.FromResult(0);
		}
	}
}