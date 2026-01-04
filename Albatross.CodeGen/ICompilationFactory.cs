using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen {
	public interface ICompilationFactory {
		Compilation Get();
	}
}
