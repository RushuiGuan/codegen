using Albatross.CommandLine.Annotations;
using System.IO;

namespace Albatross.CodeGen.CommandLine {
	/// <summary>
	/// Command-line parameters for code generation operations across multiple languages and targets
	/// </summary>
	[Verb<CSharpWebClientCodeGenCommandHandler_Client>("csharp2 web-client", Description = "Generate CSharp Http Proxy class that works with the current version of Albatross.WebClient assembly")]
	[Verb<PythonDtoCodeGenCommandHandler>("python dto")]
	[Verb<PythonWebClientCodeGenCommandHandler>("python web-client")]
	[Verb<TypeScriptDtoCodeGenCommandHandler>("typescript dto")]
	[Verb<TypeScriptWebClientCodeGenCommandHandler>("typescript web-client")]
	[Verb<ControllerInfoModelGenerator>("model controller")]
	[Verb<DtoClassInfoModelGenerator>("model dto")]
	[Verb<TypeScriptEntryPointCodeGenCommandHandler>("typescript entrypoint")]
	public record class CodeGenParams  {
		/// <summary>
		/// Gets or sets the target .NET project file to analyze for code generation
		/// </summary>
		[Option("p", Description = "Target dotnet project file")]
		public FileInfo ProjectFile { get; set; } = null!;

		/// <summary>
		/// Gets or sets an ad-hoc filter string to limit which elements are processed during code generation
		/// </summary>
		[Option("c", Description = "Ad-hoc filter for code generation")]
		public string? AdhocFilter { get; set; }

		/// <summary>
		/// Gets or sets the optional code generation settings file containing configuration
		/// </summary>
		[Option("s", Description = "Code generation settings file")]
		public FileInfo? SettingsFile { get; set; }

		/// <summary>
		/// Gets or sets the output directory for generated files
		/// </summary>
		[Option("o", Description = "Output directory for generated files")]
		public required DirectoryInfo OutputDirectory { get; init; }
	}
}