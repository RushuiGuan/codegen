using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp
{
	public interface IGetCSharpType {
		string Get(SqlType sqlType);
	}
}
