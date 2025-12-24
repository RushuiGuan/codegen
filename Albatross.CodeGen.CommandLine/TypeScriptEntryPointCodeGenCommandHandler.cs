using Albatross.CodeGen.TypeScript.Expressions;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	public class TypeScriptEntryPointCodeGenCommandHandler : CommandAction<CodeGenOptions> {
		private readonly ILogger<TypeScriptEntryPointCodeGenCommandHandler> logger;
		private readonly TypeScriptWebClientSettings settings;

		public TypeScriptEntryPointCodeGenCommandHandler(CodeGenOptions options,
			ILogger<TypeScriptEntryPointCodeGenCommandHandler> logger,
			TypeScriptWebClientSettings settings) : base(options) {
			this.logger = logger;
			this.settings = settings;
		}

		public override Task<int> Invoke(CancellationToken cancellationToken) {
			string entryFile = Path.Combine(this.options.OutputDirectory.FullName, this.settings.EntryFile);
			var sourceFoler = Path.Combine(this.options.OutputDirectory.FullName, this.settings.SourcePathRelatedToEntryFile);

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