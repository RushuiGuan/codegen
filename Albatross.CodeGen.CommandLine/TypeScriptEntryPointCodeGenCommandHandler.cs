using Albatross.CodeGen.CommandLine.Parameters;
using Albatross.CodeGen.TypeScript.Expressions;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CodeGen.WebClient.TypeScript;
using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.CommandLine.Inputs;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	[Verb<TypeScriptEntryPointCodeGenCommandHandler>("typescript entrypoint")]
	public class TypeScriptEntryPointCodeGenParams {
		
		[UseOption<CodeGenSettingsOption>]
		public CodeGenSettings? CodeGenSettings { get; init; }

		[UseOption<OutputDirectoryOption>]
		public required DirectoryInfo OutputDirectory { get; init; }
	}

	public class TypeScriptEntryPointCodeGenCommandHandler : IAsyncCommandHandler {
		private readonly TypeScriptEntryPointCodeGenParams parameters;

		public TypeScriptEntryPointCodeGenCommandHandler(TypeScriptEntryPointCodeGenParams parameters) {
			this.parameters = parameters;
		}

		public Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var settings = parameters.CodeGenSettings as TypeScriptWebClientSettings ?? new TypeScriptWebClientSettings();
			string entryFile = Path.Combine(this.parameters.OutputDirectory.FullName, settings.EntryFile);
			var sourceFoler = Path.Combine(this.parameters.OutputDirectory.FullName, settings.SourcePathRelatedToEntryFile);

			var entries = new HashSet<string>();
			if (File.Exists(entryFile)) {
				using (var reader = new StreamReader(entryFile)) {
					while (!reader.EndOfStream) {
						var line = reader.ReadLine()?.Trim();
						if (!string.IsNullOrEmpty(line)) {
							entries.Add(line);
						}
					}
				}
			}
			foreach (var file in Directory.GetFiles(sourceFoler, "*.generated.ts", SearchOption.TopDirectoryOnly)) {
				string source = $"./{settings.SourcePathRelatedToEntryFile}/{new FileInfo(file).Name}";
				var exportExpression = new ExportExpression {
					Source = new FileNameSourceExpression(source),
				};
				var writer = new StringWriter();
				exportExpression.Generate(writer);
				entries.Add(writer.ToString().Trim());
			}
			using (var writer = new StreamWriter(entryFile, false)) {
				foreach (var entry in entries) {
					writer.WriteLine(entry);
				}
			}
			return Task.FromResult(0);
		}
	}
}