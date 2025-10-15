using Albatross.CodeGen.Syntax;
using Albatross.CodeGen.Python.Expressions;
using System.Text.RegularExpressions;

namespace Albatross.CodeGen.Python {
	public static class Defined {
		public static class Patterns {
			public static Regex IdentifierName => new Regex(@"^\w\w*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			public static Regex ModuleSource => new Regex(@"^(@\w+/)? [\w\-]+ (/\w+)*$", RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.IgnoreCase);
		}
		public static class Types {
			public static SimpleTypeExpression None() => new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("None"),
			};
			public static SimpleTypeExpression Int() => new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("int")
			};
			public static SimpleTypeExpression Float() => new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("float"),
			};
			public static SimpleTypeExpression Boolean() => new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("bool"),
			};

			public static SimpleTypeExpression String() => new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("str"),
			};
			public static SimpleTypeExpression Complex() => new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("complex"),
			};
			public static SimpleTypeExpression List() => new SimpleTypeExpression {
				Identifier = new IdentifierNameExpression("string"),
			};
			public static SimpleTypeExpression Type(string name) {
				return new SimpleTypeExpression {
					Identifier = new IdentifierNameExpression(name),
				};
			}
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
			public static JsonPropertyExpression JsonProperty(string name, string value)
				=> new JsonPropertyExpression(name, new StringLiteralExpression(value));
		}
		
		public static class Decorators {
			public static DecoratorExpression Property() {
				return new DecoratorExpression {
					Identifier = new IdentifierNameExpression("property"),
				};
			}

			public static DecoratorExpression DataClass() {
				return new DecoratorExpression {
					Identifier = new QualifiedIdentifierNameExpression("dataclass", Sources.DataClasses)
				};
			}

			public static DecoratorExpression AbstractMethod() {
				return new DecoratorExpression {
					Identifier = new QualifiedIdentifierNameExpression("abstractmethod", Sources.DataClasses)
				};
			}
		}

		public static class Invocations {
			public static InvocationExpression EnumAuto()
				=> new InvocationExpression {
					Identifier = new QualifiedIdentifierNameExpression("auto", Sources.Enum)
				};
		}

		public static class Sources {
			public static readonly ISourceExpression DataClasses = new ModuleSourceExpression("dataclasses");
			public static readonly ISourceExpression Enum = new ModuleSourceExpression("enum");
			public static readonly ISourceExpression AbstractBaseClass = new ModuleSourceExpression("abc");
		}

		public static class Identifiers {
			public static readonly IIdentifierNameExpression Self = new IdentifierNameExpression("self");
			public static readonly IIdentifierNameExpression Enum = new QualifiedIdentifierNameExpression("Enum", Sources.Enum);
			public static readonly IIdentifierNameExpression AbstractBaseClass = new QualifiedIdentifierNameExpression("ABC", Sources.AbstractBaseClass);
		}
	}
}