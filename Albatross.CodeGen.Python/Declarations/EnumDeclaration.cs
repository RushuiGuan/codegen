using Albatross.CodeGen.Python.Expressions;
using Albatross.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Albatross.CodeGen.Python.Declarations {
	public record class EnumDeclaration : ClassDeclaration {
		public EnumDeclaration(string name) : base(name) {
			BaseClassName = Defined.Identifiers.Enum;
		}
	}
}