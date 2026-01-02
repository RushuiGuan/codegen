using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Python;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CodeGen.WebClient.TypeScript;
using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.CommandLine.Inputs;
using System.CommandLine;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine.Parameters {
	[DefaultNameAliases("--codegen-settings", "-s")]
	[OptionHandler(typeof(LoadCodeGenSettings))]
	public class CodeGenSettingsOption : InputFileOption, IUseContextValue {
		public CodeGenSettingsOption(string name, params string[] aliases) : base(name, aliases) { }
	}

	public class LoadCodeGenSettings : IAsyncOptionHandler<CodeGenSettingsOption> {
		private readonly ICommandContext context;

		public LoadCodeGenSettings(ICommandContext context) {
			this.context = context;
		}
		public async Task InvokeAsync(CodeGenSettingsOption symbol, ParseResult result, CancellationToken cancellationToken) {
			var file = result.GetValue(symbol);
			if (file != null) {
				using var stream = file.OpenRead();
				CodeGenSettings settings;
				if (context.Key.StartsWith("csharp")) {
					settings = await JsonSerializer.DeserializeAsync<CSharpWebClientSettings>(stream, cancellationToken: cancellationToken);
				} else if (context.Key.StartsWith("typescript")) {
					settings = await JsonSerializer.DeserializeAsync<TypeScriptWebClientSettings>(stream, cancellationToken: cancellationToken);
				} else if (context.Key.StartsWith("python")) {
					settings = await JsonSerializer.DeserializeAsync<PythonWebClientSettings>(stream, cancellationToken: cancellationToken);
				} else {
					settings = await JsonSerializer.DeserializeAsync<CodeGenSettings>(stream, cancellationToken: cancellationToken);
				}
				context.SetValue(symbol.Name, settings);
			}
		}
	}
}
