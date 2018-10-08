using Albatross.CodeGen.CSharp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class WriteParameter : IWriteObject<Parameter> {
		IWriteObject<DotNetType> writeType;

		public WriteParameter(IWriteObject<DotNetType> writeType) {
			this.writeType = writeType;
		}

		public string Write(Parameter t) {
			return $"{writeType.Write(t.Type)} @{t.Name}";
		}
	}
}
