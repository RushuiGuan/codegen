using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	/// <summary>
	/// factory to return all the registered source types.
	/// </summary>
	public interface ISourceTypeFactory {
		IEnumerable<SourceType> Get();
	}
}
