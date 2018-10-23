using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp.Core {
	public interface IDatabaseTableToClass {
		Class Get(Table table);
	}
}
