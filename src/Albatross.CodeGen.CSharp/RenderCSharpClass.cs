using System;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.CSharp.Model;

namespace Albatross.CodeGen.CSharp
{
    /// <summary>
    /// Render a CSharp Class from a Class model
    /// </summary>
    [CodeGenerator("csharp-class", GeneratorTarget.CSharp, Description ="Render a CSharp class from model")]
	public class RenderCSharpClass : ICodeGenerator<Class, object> {
		IWriteObject<Class> writeClass;

		public RenderCSharpClass(IWriteObject<Class> writeClass) {
			this.writeClass = writeClass;
		}

        public event Func<StringBuilder, IEnumerable<object>> Yield;

        public IEnumerable<object> Build(StringBuilder sb, Class source, object option) {
			sb.Write(writeClass, source);
			return new object[] { this };
		}

        public IEnumerable<object> Build(StringBuilder sb, object source, object option)
        {
            sb.Write(writeClass, (Class)source);
            return new object[] { this };
        }

        public void Configure(object data)
        {
        }
    }
}