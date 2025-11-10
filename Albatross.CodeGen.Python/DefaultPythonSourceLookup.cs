using Albatross.CodeAnalysis.Symbols;
using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.Syntax;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Albatross.CodeGen.Python {
	public class DefaultPythonSourceLookup : ISourceLookup {
		private readonly Dictionary<string, string> source;

		public DefaultPythonSourceLookup(Dictionary<string, string> source) {
			this.source = source;
		}

		public bool TryGet(ITypeSymbol symbol, [NotNullWhen(true)] out ISourceExpression? module) {
			if (symbol is INamedTypeSymbol named) {
				var fullName = named.GetFullName();
				foreach (var key in source.Keys) {
					if (fullName.StartsWith(key, StringComparison.InvariantCultureIgnoreCase)) {
						var value = source[key];
						module = new ModuleSourceExpression(value);
						return true;
					}
				}
			}
			module = null;
			return false;
		}
	}
}