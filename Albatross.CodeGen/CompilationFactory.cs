using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen {
	public class CompilationFactory : ICompilationFactory {
		private readonly Compilation compilation;

		public CompilationFactory(Compilation compilation) {
			this.compilation = compilation;
		}
		public Compilation Get() => compilation;
	}
}
