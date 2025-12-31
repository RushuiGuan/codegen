using Albatross.CommandLine.Annotations;

namespace Albatross.CodeGen.CommandLine {
	/// <summary>
	/// Parent command parameters that organize code generation commands by language and target type
	/// </summary>
	[Verb("csharp", Alias = ["cs"], Description = "Commands to generate csharp code")]
	[Verb("typescript", Alias = ["ts"], Description = "Commands to generate typescript code")]
	[Verb("python", Alias = ["py"], Description = "Commands to generate python code")]
	[Verb("model", Alias = ["m"], Description = "Produce codegen models for analysis or further code generation")]
	[Verb("schema", Alias = ["s"], Description = "Produce schema for codegen settings")]
	public class ParentOptions {
	}
}