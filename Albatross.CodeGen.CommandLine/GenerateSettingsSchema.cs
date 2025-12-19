using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Python;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine;
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
		[Option(Description ="The output file for the generated schema")]
		public FileInfo? File { get; set; }
	}

	public class GenerateSettingsSchema : CommandAction<GenerateSettingsSchemaOptions> {
		public GenerateSettingsSchema(ParseResult result, GenerateSettingsSchemaOptions options) : base(result, options) {
		}

		public override Task<int> Invoke(CancellationToken cancellationToken) {
			var settings = new SystemTextJsonSchemaGeneratorSettings {
				FlattenInheritanceHierarchy = true,
				SerializerOptions = new System.Text.Json.JsonSerializerOptions {
					WriteIndented = true,
					PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
				},
			};
			var generator = new JsonSchemaGenerator(settings);
			var schema = Result.CommandResult.Command.Name switch {
				"python" => generator.Generate(typeof(PythonWebClientSettings)),
				"typescript" => generator.Generate(typeof(TypeScriptWebClientSettings)),
				"csharp" => generator.Generate(typeof(CSharpWebClientSettings)),
				_ => throw new NotSupportedException(),
			};
			schema.Properties.Add("$schema", new JsonSchemaProperty {
				Type = JsonObjectType.String,
			});
			var text = schema.ToJson();
			System.Console.WriteLine(text);
			if (options.File != null) {
				System.IO.File.WriteAllText(options.File.FullName, text);
			}
			return Task.FromResult(0);
		}
	}
}