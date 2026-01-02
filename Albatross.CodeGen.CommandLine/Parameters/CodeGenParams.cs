using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine.Annotations;
using Albatross.CommandLine.Inputs;
using Microsoft.CodeAnalysis;
using System.IO;

namespace Albatross.CodeGen.CommandLine.Parameters {
	/// <summary>
	/// Command-line parameters for code generation operations across multiple languages and targets
	/// </summary>
	[Verb<CSharpWebClientCodeGenCommandHandler_Client>("csharp web-client", Description = "Generate CSharp Http Proxy class that works with the current version of Albatross.WebClient assembly")]
	[Verb<PythonDtoCodeGenCommandHandler>("python dto")]
	[Verb<PythonWebClientCodeGenCommandHandler>("python web-client")]
	[Verb<TypeScriptDtoCodeGenCommandHandler>("typescript dto")]
	[Verb<TypeScriptWebClientCodeGenCommandHandler>("typescript web-client")]
	[Verb<ControllerInfoModelGenerator>("model controller")]
	[Verb<DtoClassInfoModelGenerator>("model dto")]
	public record class CodeGenParams {
		/// <summary>
		/// Gets or sets the target .NET project file to analyze for code generation
		/// </summary>
		[UseOption<ProjectCompilationOption>]
		public required Compilation Compilation { get; init; }

		/// <summary>
		/// Gets or sets an ad-hoc filter string to limit which elements are processed during code generation
		/// </summary>
		[Option("c", Description = "Ad-hoc filter for code generation")]
		public string? AdhocFilter { get; set; }

		/// <summary>
		/// Gets or sets the optional code generation settings file containing configuration
		/// </summary>
		[UseOption<CodeGenSettingsOption>]
		public CodeGenSettings? CodeGenSettings { get; set; }

		/// <summary>
		/// Gets or sets the output directory for generated files
		/// </summary>
		[UseOption<OutputDirectoryOption>]
		public DirectoryInfo? OutputDirectory { get; init; }
	}
}