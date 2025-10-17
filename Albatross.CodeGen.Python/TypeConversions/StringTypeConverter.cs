﻿using Albatross.CodeGen.Python.Expressions;
using Albatross.CodeGen.Syntax;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace Albatross.CodeGen.Python.TypeConversions {
	public class StringTypeConverter : SimpleTypeConverter {
		protected override IEnumerable<string> NamesToMatch => [
			"System.String",
			"System.Char",
			"System.Char[]"
		];

		protected override ITypeExpression GetResult(ITypeSymbol symbol) 
			=> symbol.NullableAnnotation == NullableAnnotation.Annotated
				? new MultiTypeExpression(Defined.Types.None, Defined.Types.String)
				: Defined.Types.String;
	}
}