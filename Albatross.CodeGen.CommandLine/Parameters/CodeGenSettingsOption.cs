using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Python;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CodeGen.WebClient.TypeScript;
using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.CommandLine.Inputs;
using System.CommandLine;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine.Parameters {
	[OptionHandler(typeof(LoadCodeGenSettings))]
	public class CodeGenSettingsOption : InputFileOption, IUseContextValue {
		public CodeGenSettingsOption() : this("--codegen-settings", "--cs") { }
		public CodeGenSettingsOption(string name, params string[] aliases) : base(name, aliases) { }
	}

	public class LoadCodeGenSettings : IAsyncOptionHandler<CodeGenSettingsOption> {
		static JsonSerializerOptions options = new JsonSerializerOptions {
			TypeInfoResolver = new DefaultJsonTypeInfoResolver {
				Modifiers = { (typeInfo) =>{
					if (typeInfo.Type == typeof(CodeGenSettings)) {
				typeInfo.PolymorphismOptions = new JsonPolymorphismOptions {
					TypeDiscriminatorPropertyName = "type",
					UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
					DerivedTypes = {
						new JsonDerivedType(typeof(CSharpWebClientSettings), "csharp"),
						new JsonDerivedType(typeof(TypeScriptWebClientSettings), "typescript"),
						new JsonDerivedType(typeof(PythonWebClientSettings), "python"),
					}
				};
			}
				}}
			}
		};
		private readonly ICommandContext context;

		public LoadCodeGenSettings(ICommandContext context) {
			this.context = context;
		}
		public async Task InvokeAsync(CodeGenSettingsOption symbol, ParseResult result, CancellationToken cancellationToken) {
			var file = result.GetValue(symbol);
			if (file != null) {
				using var stream = file.OpenRead();
				var settings = await JsonSerializer.DeserializeAsync<CodeGenSettings>(stream, options, cancellationToken);
				context.SetValue(symbol.Name, settings);
			}
		}
	}
}
