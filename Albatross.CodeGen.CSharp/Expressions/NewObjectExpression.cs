using Albatross.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Albatross.CodeGen.CSharp.Expressions {
	public record class NewObjectExpression : InvocationExpression{
		public override TextWriter Generate(TextWriter writer) {
			writer.Append("new ");
			return base.Generate(writer);
		}
	}
}
