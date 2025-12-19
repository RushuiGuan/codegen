using Albatross.CommandLine;
using System.IO;

namespace Albatross.CodeGen.CommandLine {
#pragma warning disable CS0612 // Type or member is obsolete
	[Verb<CSharpWebClientCodeGenCommandHandler_Client800>("csharp web-client", Description = "Generate CSharp Http Proxy class that works with the current version of Albatross.WebClient assembly")]
	[Verb<CSharpWebClientCodeGenCommandHandler_Client740>("csharp web-client740", Description = "Generate CSharp Http Proxy class that works with Albatross.WebClient assembly version 7.4.*")]
	[Verb<CSharpWebClientCodeGenCommandHandler_Client402>("csharp web-client402", Description = "Generate CSharp Http Proxy class that works with Albatross.WebClient assembly version 4.0.*")]
#pragma warning restore CS0612 // Type or member is obsolete
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