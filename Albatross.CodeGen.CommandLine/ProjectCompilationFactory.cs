using Albatross.CodeGen.WebClient;
using Albatross.CodeGen.CommandLine.Parameters;
using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen.CommandLine {
	public class ProjectCompilationFactory : ICompilationFactory {
		private readonly CodeGenParams @params;

		public ProjectCompilationFactory(CodeGenParams @params) {
			this.@params = @params;
		}

		public Compilation Get() => @params.Compilation;
	}
}