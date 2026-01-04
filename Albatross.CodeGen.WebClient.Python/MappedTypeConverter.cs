using Albatross.CodeAnalysis;
using Albatross.CodeGen.Python.Expressions;
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.CodeGen.WebClient.Python {
	public class MappedTypeConverter : ITypeConverter {
		private PythonWebClientSettings settings;

		public MappedTypeConverter(ICodeGenSettingsFactory settingsFactory) {
			this.settings = settingsFactory.Get<PythonWebClientSettings>();
		}
		// this should have higher precedence than CustomTypeConversion
		public int Precedence => 998;

		public bool TryConvert(ITypeSymbol symbol, IConvertObject<ITypeSymbol, ITypeExpression> factory, [NotNullWhen(true)] out ITypeExpression? expression) {
			if (settings.TypeMapping.TryGetValue(symbol.GetFullName(), out var mappedType)) {
				expression = new SimpleTypeExpression {
					Identifier = mappedType.ParseIdentifierName(),
				};
				return true;
			} else {
				expression = null;
				return false;
			}
		}
	}
}