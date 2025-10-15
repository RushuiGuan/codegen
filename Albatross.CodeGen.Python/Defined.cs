using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Expressions;
using System.Text.RegularExpressions;

namespace Albatross.CodeGen.Python {
	public static class Defined {
		public static class Types {
			public static readonly SimpleTypeExpression None = new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("None"),
			};
			
			public static readonly SimpleTypeExpression Any = new SimpleTypeExpression {
				Identifier = new QualifiedIdentifierNameExpression("Any", Sources.Typing),
			};

			public static readonly SimpleTypeExpression Int = new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("int")
			};

			public static readonly SimpleTypeExpression Float = new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("float"),
			};

			public static readonly SimpleTypeExpression Boolean = new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("bool"),
			};

			public static readonly SimpleTypeExpression String = new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("str"),
			};

			public static readonly SimpleTypeExpression Complex = new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("complex"),
			};
			
			public static readonly SimpleTypeExpression List = new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("list"),
			};
			
			public static readonly SimpleTypeExpression Dictionary = new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("dict"),
			};
			
			public static readonly SimpleTypeExpression Tuple = new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("tuple"),
			};
			
			public static readonly SimpleTypeExpression Set = new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("set"),
			};

			public static SimpleTypeExpression Type(string name) => new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression(name),
			};
		}

		public static class Literals {
			public static StringLiteralExpression String(string value)
				=> new StringLiteralExpression(value);

			public static NumberLiteralExpression Number(int value)
				=> new NumberLiteralExpression(value);

			public static NumberLiteralExpression NumberLiteral(double value)
				=> new NumberLiteralExpression(value);

			public static BooleanLiteralExpression BooleanLiteral(bool value)
				=> new BooleanLiteralExpression(value);
		}

		public static class Decorators {
			public static readonly DecoratorExpression Property = new DecoratorExpression {
				Identifier = new IdentifierNameExpression("property"),
			};

			public static readonly DecoratorExpression DataClass = new DecoratorExpression {
				Identifier = new QualifiedIdentifierNameExpression("dataclass", Sources.DataClasses)
			};

			public static readonly DecoratorExpression AbstractMethod = new DecoratorExpression {
				Identifier = new QualifiedIdentifierNameExpression("abstractmethod", Sources.DataClasses)
			};
		}

		public static class Invocations {
			public static readonly InvocationExpression EnumAuto = new InvocationExpression {
				Identifier = new QualifiedIdentifierNameExpression("auto", Sources.Enum)
			};
		}

		public static class Sources {
			public static readonly ISourceExpression DataClasses = new ModuleSourceExpression("dataclasses");
			public static readonly ISourceExpression Enum = new ModuleSourceExpression("enum");
			public static readonly ISourceExpression AbstractBaseClass = new ModuleSourceExpression("abc");
			public static readonly ISourceExpression Typing = new ModuleSourceExpression("typing");
		}

		public static class Identifiers {
			public static readonly IIdentifierNameExpression Self = new IdentifierNameExpression("self");
			public static readonly IIdentifierNameExpression Enum = new QualifiedIdentifierNameExpression("Enum", Sources.Enum);
			public static readonly IIdentifierNameExpression AbstractBaseClass = new QualifiedIdentifierNameExpression("ABC", Sources.AbstractBaseClass);
		}
	}
}