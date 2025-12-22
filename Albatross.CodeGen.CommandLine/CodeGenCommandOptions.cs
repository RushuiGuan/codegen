using Albatross.CommandLine;
using System.IO;

namespace Albatross.CodeGen.CommandLine {
	[Verb<CSharpWebClientCodeGenCommandHandler_Client>("csharp2 web-client", Description = "Generate CSharp Http Proxy class that works with the current version of Albatross.WebClient assembly")]
	[Verb<PythonDtoCodeGenCommandHandler>("python dto")]
	[Verb<PythonWebClientCodeGenCommandHandler>("python web-client")]
	[Verb<TypeScriptDtoCodeGenCommandHandler>("typescript dto")]
	[Verb<TypeScriptWebClientCodeGenCommandHandler>("typescript web-client")]
	[Verb<ControllerInfoModelGenerator>("model controller")]
	[Verb<DtoClassInfoModelGenerator>("model dto")]
	public record class CodeGenCommandOptions {
		[Option("p", Description ="Target dotnet project file")]
		public FileInfo ProjectFile { get; set; } = null!;

		[Option("s", Description ="Code generation settings file")]
		public FileInfo? SettingsFile { get; set; }

		[Option("o", Description ="Output directory for generated files")]
		public DirectoryInfo? OutputDirectory { get; set; }

		[Option("c", Description ="Ad-hoc filter for code generation")]
		public string? AdhocFilter { get; set; }
	}
}