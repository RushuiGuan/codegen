using Albatross.CodeGen.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen
{
	public interface IGetCSharpType {
		Type Get(Column c);
	}
}
