using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.CSharp.Keywords;
using Albatross.CodeGen.Syntax;

namespace Albatross.CodeGen.CSharp {
	public static class Defined {
		public static class Types {
			public static readonly ITypeExpression Int = new TypeExpression("int");
			public static readonly ITypeExpression Long = new TypeExpression("long");
			public static readonly ITypeExpression String = new TypeExpression("string");
			public static readonly ITypeExpression Bool = new TypeExpression("bool");
			public static readonly ITypeExpression Double = new TypeExpression("double");
			public static readonly ITypeExpression Float = new TypeExpression("float");
			public static readonly ITypeExpression Decimal = new TypeExpression("decimal");
			public static readonly ITypeExpression Object = new TypeExpression("object");
			public static readonly ITypeExpression Void = new TypeExpression("void");
			public static readonly ITypeExpression Var = new TypeExpression("var");
			public static readonly ITypeExpression Task = new TypeExpression("System.Threading.Tasks.Task");
		}

		public static class Keywords {
			public static readonly AccessModifierKeyword Public = new AccessModifierKeyword("public");
			public static readonly AccessModifierKeyword Private = new AccessModifierKeyword("private");
			public static readonly AccessModifierKeyword Protected = new AccessModifierKeyword("protected");
			public static readonly AccessModifierKeyword Internal = new AccessModifierKeyword("internal");

			public static readonly IKeyword Abstract = new Keyword("abstract");
			public static readonly IKeyword Async = new Keyword("async");
			public static readonly IKeyword Await = new Keyword("await");
			public static readonly IKeyword Extern = new Keyword("extern");
			public static readonly IKeyword Const = new Keyword("const");
			public static readonly IKeyword Override = new Keyword("override");
			public static readonly IKeyword Sealed = new Keyword("sealed");
			public static readonly IKeyword Static = new Keyword("static");
			public static readonly IKeyword Virtual = new Keyword("virtual");
			public static readonly IKeyword Partial = new Keyword("partial");
			public static readonly IKeyword Readonly = new Keyword("readonly");
			public static readonly IKeyword Return = new Keyword("return");
		}
		
		public static class Attributes {
			public static readonly AttributeExpression Obsolete = new AttributeExpression {
				CallableExpression = new IdentifierNameExpression("System.ObsoleteAttribute")
			};
		}
	}
}