using Albatross.CodeGen.WebClient.CSharp;
using Albatross.CodeGen.WebClient.Python;
using Albatross.CodeGen.WebClient.Settings;
using Albatross.CodeGen.WebClient.TypeScript;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace Albatross.CodeGen.CommandLine {
	internal class SerializationOptions {
		public static void AddPolymorphismConfiguration(JsonTypeInfo typeInfo) {
			if (typeInfo.Type == typeof(CodeGenSettings)) {
				typeInfo.PolymorphismOptions = new JsonPolymorphismOptions {
					TypeDiscriminatorPropertyName = "$type",
					IgnoreUnrecognizedTypeDiscriminators = true,
					UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization,
					DerivedTypes =
					{
				new JsonDerivedType(typeof(CSharpWebClientSettings), "csharp"),
				new JsonDerivedType(typeof(TypeScriptWebClientSettings), "typescript"),
				new JsonDerivedType(typeof(PythonWebClientSettings), "python")
			}
				};
			}
		}
	}
}
