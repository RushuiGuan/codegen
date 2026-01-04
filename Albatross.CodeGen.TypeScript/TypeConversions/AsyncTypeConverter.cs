using Albatross.CodeAnalysis;
using Albatross.CodeGen.SymbolProviders;
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.CodeGen.TypeScript.TypeConversions {
	public class AsyncTypeConverter : ITypeConverter {
		private readonly Compilation compilation;
		public int Precedence => 90;

		public AsyncTypeConverter(ICompilationFactory factory) {
			this.compilation = factory.Get();
		}

		public bool TryConvert(ITypeSymbol symbol, IConvertObject<ITypeSymbol, ITypeExpression> factory, [NotNullWhen(true)] out ITypeExpression? expression) {
			var name = symbol.GetFullName();
			if (name == "System.Threading.Tasks.Task") {
				expression = Defined.Types.Void();
				return true;
			} else if (symbol.TryGetGenericTypeArguments(compilation.TaskGenericDefinition(), out var arguments)) {
				expression = factory.Convert(arguments[0]);
				return true;
			} else {
				expression = null;
				return false;
			}
		}
	}
}