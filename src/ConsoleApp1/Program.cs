using NSwag.CodeGeneration.CSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp1 {
	class Program {
		static void Main(string[] args) {
			Task task = Task.Run(async () => await Run());
			Task.WaitAll(task);
		}

		static async Task Run() {
			string json;
			using (StreamReader reader = new StreamReader(@"C:\git\codegen\src\ConsoleApp1\data.json")) {
				json = reader.ReadToEnd();
			}
			var document = await NSwag.SwaggerDocument.FromJsonAsync(json);
			var settings = new SwaggerToCSharpClientGeneratorSettings {
				ClassName = "MyClass",
				CSharpGeneratorSettings = { Namespace = "MyNamespace" }
			};
			var generator = new SwaggerToCSharpClientGenerator(document, settings);
			var code = generator.GenerateFile();
			using (StreamWriter writer = new StreamWriter(@"C:\git\codegen\src\ConsoleApp1\output.cs")) {
				writer.Write(code);
			}
		}
	}
}
