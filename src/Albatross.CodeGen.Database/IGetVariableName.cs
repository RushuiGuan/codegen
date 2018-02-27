using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Database {
	/// <summary>
	/// create a valid sql variable name
	/// </summary>
	public interface IGetVariableName {
		string Get(string name);
	}
}
