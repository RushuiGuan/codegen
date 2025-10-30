using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.Python.Declarations;
using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.WebClient.Models;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CodeGen.WebClient.Python;
using Albatross.CommandLine;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.CommandLine {
	public class PythonDtoCodeGenCommandHandler : BaseHandler<CodeGenCommandOptions> {
		private readonly Compilation compilation;
		private readonly CodeGenSettings settings;
		private readonly ConvertClassSymbolToDtoClassModel dto2Model;
		private readonly ConvertEnumSymbolToDtoEnumModel enum2Model;
		private readonly ConvertDtoClassModelToDataClass dtoModel2Python;
		private readonly ConvertEnumModelToPythonEnum enumModel2Python;

		public PythonDtoCodeGenCommandHandler(Compilation compilation,
			CodeGenSettings settings,
			ConvertClassSymbolToDtoClassModel dto2Model,
			ConvertEnumSymbolToDtoEnumModel enum2Model,
			ConvertDtoClassModelToDataClass dtoModel2Python,
			ConvertEnumModelToPythonEnum enumModel2Python,
			IOptions<CodeGenCommandOptions> options) :base(options){
			this.compilation = compilation;
			this.settings = settings;
			this.dto2Model = dto2Model;
			this.enum2Model = enum2Model;
			this.dtoModel2Python = dtoModel2Python;
			this.enumModel2Python = enumModel2Python;
		}

		public override int Invoke(InvocationContext context) {
			var dtoModels = new List<DtoClassInfo>();
			var enumModels = new List<EnumInfo>();
			foreach (var syntaxTree in compilation.SyntaxTrees) {
				var semanticModel = compilation.GetSemanticModel(syntaxTree);
				var symbolWalker = new DtoClassEnumWalker(semanticModel, settings.CreatePythonDtoFilter());
				symbolWalker.Visit(syntaxTree.GetRoot());
				dtoModels.AddRange(symbolWalker.DtoClasses
					.Where(x => string.IsNullOrEmpty(options.AdhocFilter) || x.GetFullName().Contains(options.AdhocFilter, System.StringComparison.InvariantCultureIgnoreCase))
					.Select(x => dto2Model.Convert(x)));

				enumModels.AddRange(symbolWalker.EnumTypes
					.Where(x => string.IsNullOrEmpty(options.AdhocFilter) || x.GetFullName().Contains(options.AdhocFilter, System.StringComparison.InvariantCultureIgnoreCase))
					.Select(x => enum2Model.Convert(x)));
			}
			var dtoFile = new PythonFileDeclaration("dto") {
				ClasseDeclarations = enumModels.Select(x => enumModel2Python.Convert(x))
					.Concat(dtoModels.Select(x => dtoModel2Python.Convert(x)))
					.ToList(),
			};
			dtoFile.Generate(System.Console.Out);
			if (options.OutputDirectory != null) {
				var file = Path.Join(options.OutputDirectory.FullName, dtoFile.FileName);
				using var stream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
				using var streamWriter = new StreamWriter(stream);
				dtoFile.Generate(streamWriter);
				streamWriter.Flush();
				stream.SetLength(stream.Position);
			}
			return 0;
		}
	}
}