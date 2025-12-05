using Albatross.CommandLine;
using System.IO;

namespace Albatross.CodeGen.CommandLine {
	[Verb("csharp2 web-client", typeof(CSharpWebClientCodeGenCommandHandler_Client), Description = "Generate CSharp Http Proxy class that works with the current version of Albatross.WebClient assembly")]
	[Verb("csharp web-client", typeof(CSharpWebClientCodeGenCommandHandler_Client800), Description = "Generate CSharp Http Proxy class that works with the current version of Albatross.WebClient assembly")]
	[Verb("csharp web-client740", typeof(CSharpWebClientCodeGenCommandHandler_Client740), Description = "Generate CSharp Http Proxy class that works with Albatross.WebClient assembly version 7.4.*")]
	[Verb("csharp web-client402", typeof(CSharpWebClientCodeGenCommandHandler_Client402), Description = "Generate CSharp Http Proxy class that works with Albatross.WebClient assembly version 4.0.*")]
	[Verb("python dto", typeof(PythonDtoCodeGenCommandHandler))]
	[Verb("python web-client", typeof(PythonWebClientCodeGenCommandHandler))]
	[Verb("typescript dto", typeof(TypeScriptDtoCodeGenCommandHandler))]
	[Verb("typescript web-client", typeof(TypeScriptWebClientCodeGenCommandHandler))]
	[Verb("model controller", typeof(ControllerInfoModelGenerator))]
	[Verb("model dto", typeof(DtoClassInfoModelGenerator))]
	public record class CodeGenCommandOptions {
		[Option("p")]
		public FileInfo ProjectFile { get; set; } = null!;

		[Option("s")]
		public FileInfo? SettingsFile { get; set; }

		[Option("o")]
		public DirectoryInfo? OutputDirectory { get; set; }

		[Option("c")]
		public string? AdhocFilter { get; set; }
	}
}