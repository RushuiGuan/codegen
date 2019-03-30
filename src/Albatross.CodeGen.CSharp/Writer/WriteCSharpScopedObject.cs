using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer
{
    public class WriteCSharpScopedObject : WriteScopedObject<string> {

		public WriteCSharpScopedObject(StringBuilder parent) : base(parent) {
		}

		public override IWriteScopedObject<string> BeginScope(string t = "") {
			Parent.Append(t).AppendLine(" {");
			return this;
		}
		public override IWriteScopedObject<string> BeginChildScope(string t = "") {
			var childScope = new WriteCSharpScopedObject(Content);
			return childScope.BeginScope(t);
		}
		public override void EndScope() {
			Parent.Append("}");
		}
	}
}