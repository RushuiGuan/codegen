using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.SymbolProviders;
using Albatross.CodeGen.Syntax;
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class AsyncTypeConverter : ITypeConverter {
		private readonly Compilation compilation;
		public int Precedence => 90;

		public AsyncTypeConverter(Compilation compilation) {
			this.compilation = compilation;
		}

		public bool TryConvert(ITypeSymbol symbol, IConvertObject<ITypeSymbol, ITypeExpression> factory, [NotNullWhen(true)] out ITypeExpression? expression) {
			var name = symbol.GetFullName();
			if (name == "System.Threading.Tasks.Task") {
				expression = Defined.Types.None;
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