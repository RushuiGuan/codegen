using Albatross.CodeGen.WebClient.Settings;
using Albatross.CommandLine.Annotations;
using Albatross.CommandLine.Inputs;
using Microsoft.CodeAnalysis;
using System.IO;

namespace Albatross.CodeGen.CommandLine.Parameters {
	/// <summary>
	/// Command-line parameters for code generation operations across multiple languages and targets
	/// </summary>
	[Verb<LegacyCSharpWebClientCodeGenCommandHandler>("csharp legacy-web-client", Description = "Generate CSharp Http WebClient class that works with the current version of Albatross.WebClient assembly")]
	[Verb<CSharpWebClientCodeGenCommandHandler>("csharp web-client", Description = "Generate CSharp Http WebClient class that works with the current version of Albatross.Http assembly")]
	[Verb<PythonDtoCodeGenCommandHandler>("python dto", Description = "Generate Python DTO class using Pydantic")]
	[Verb<PythonWebClientCodeGenCommandHandler>("python web-client", Description = "Generate Python webclient library for a AspnetCore WebApi project")]
	[Verb<TypeScriptDtoCodeGenCommandHandler>("typescript dto", Description = "Genreate TypeScript Dto classes")]
	[Verb<TypeScriptWebClientCodeGenCommandHandler>("typescript web-client", Description = "Generate TypeScript webclient library for a aspnetcore WebApi project")]
	[Verb<ControllerInfoModelGenerator>("model controller", Description = "Generate the controller model")]
	[Verb<DtoClassInfoModelGenerator>("model dto", Description = "Generate the dto model")]
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
		[UseOption<OutputDirectoryWithAutoCreateOption>]
		public DirectoryInfo? OutputDirectory { get; init; }
	}
}