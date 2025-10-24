using Albatross.CommandLine;

namespace Albatross.CodeGen.CommandLine {
	[Verb("csharp", Alias = ["cs"], Description = "Commands to generate csharp code")]
	[Verb("typescript", Alias = ["ts"], Description = "Commands to generate typescript code")]
	[Verb("python", Alias = ["py"], Description = "Commands to generate python code")]
	[Verb("model", Alias = ["m"], Description = "Produce codegen models for analysis or further code generation")]
	public class ParentOptions {
	}
}