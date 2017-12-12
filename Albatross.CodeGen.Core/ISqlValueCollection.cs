using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Core {
	public interface ISqlValueCollection : ICodeSource {
		IEnumerable<SqlValue> Values { get; }
	}
}
