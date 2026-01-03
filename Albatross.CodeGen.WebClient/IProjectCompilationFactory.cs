using Microsoft.CodeAnalysis;

namespace Albatross.CodeGen.WebClient {
	public interface IProjectCompilationFactory {
		Compilation Get();
	}
}
