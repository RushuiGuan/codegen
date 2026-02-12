using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Python;
using Albatross.CodeGen.WebClient.TypeScript;
using Albatross.CommandLine;
using Albatross.CommandLine.Annotations;
using Albatross.CommandLine.Inputs;
using NJsonSchema;
using NJsonSchema.Generation;
using System;
using System.CommandLine;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	[Verb<GenerateSettingsSchema>("schema python")]
	[Verb<GenerateSettingsSchema>("schema csharp")]
	[Verb<GenerateSettingsSchema>("schema typescript")]
	public record class GenerateSettingsSchemaOptions {
		[UseOption<OutputFileOption>]
		public FileInfo? OutputFile { get; set; }
	}

	public class GenerateSettingsSchema : BaseHandler<GenerateSettingsSchemaOptions> {
		public GenerateSettingsSchema(ParseResult result, GenerateSettingsSchemaOptions parameters) : base(result, parameters) {
		}

		public override Task<int> InvokeAsync(CancellationToken cancellationToken) {
			var settings = new SystemTextJsonSchemaGeneratorSettings {
				FlattenInheritanceHierarchy = true,
				SerializerOptions = new System.Text.Json.JsonSerializerOptions {
					WriteIndented = true,
					PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
				},
			};
			var generator = new JsonSchemaGenerator(settings);
			var schema = result.CommandResult.Command.Name switch {
				"python" => generator.Generate(typeof(PythonWebClientSettings)),
				"typescript" => generator.Generate(typeof(TypeScriptWebClientSettings)),
				"csharp" => generator.Generate(typeof(CSharpWebClientSettings)),
				_ => throw new NotSupportedException(),
			};
			schema.Properties.Add("$schema", new JsonSchemaProperty {
				Type = JsonObjectType.String,
			});
			var text = schema.ToJson();
			if (parameters.OutputFile != null) {
				System.IO.File.WriteAllText(parameters.OutputFile.FullName, text);
			} else {
				System.Console.WriteLine(text);

			}
			return Task.FromResult(0);
		}
	}
}