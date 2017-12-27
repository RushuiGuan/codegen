using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public interface ISourceTypeFactory {
		IEnumerable<Type> Get();
	}
}
