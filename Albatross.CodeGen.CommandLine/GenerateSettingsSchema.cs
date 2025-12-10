using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Python;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine;
using Microsoft.Extensions.Options;
using NJsonSchema;
using NJsonSchema.Generation;
using System;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading.Tasks;

namespace Albatross.CodeGen.CommandLine {
	[Verb("schema python", typeof(GenerateSettingsSchema))]
	[Verb("schema csharp", typeof(GenerateSettingsSchema))]
	[Verb("schema typescript", typeof(GenerateSettingsSchema))]
	public record class GenerateSettingsSchemaOptions {
		public FileInfo? File { get; set; }
	}

	public class GenerateSettingsSchema : BaseHandler<GenerateSettingsSchemaOptions> {
		public GenerateSettingsSchema(IOptions<GenerateSettingsSchemaOptions> options) : base(options) {
		}

		public override Task<int> InvokeAsync(InvocationContext context) {
			var settings = new SystemTextJsonSchemaGeneratorSettings {
				FlattenInheritanceHierarchy = true,
				SerializerOptions = new System.Text.Json.JsonSerializerOptions {
					WriteIndented = true,
					PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
				},
			};
			var generator = new JsonSchemaGenerator(settings);
			var schema = context.ParsedCommandName() switch {
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