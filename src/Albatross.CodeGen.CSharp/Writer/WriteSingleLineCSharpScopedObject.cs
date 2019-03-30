using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer
{
    public class WriteSingleLineCSharpScopedObject : WriteScopedObject<string> {

		public WriteSingleLineCSharpScopedObject(StringBuilder parent) : base(parent) {
		}

		public override IWriteScopedObject<string> BeginScope(string t = "") {
			Parent.Append(t).Append(" {");
			return this;
		}
		public override IWriteScopedObject<string> BeginChildScope(string t) {
			var childScope = new WriteSingleLineCSharpScopedObject(Content);
			return childScope.BeginScope(t);
		}

		public override void WriteContent() {
			Parent.Append(Content);
		}

		public override void EndScope() {
			Parent.Append("}");
		}
	}
}