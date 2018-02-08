using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public interface IComposite {
		Type SourceType { get; }
		Type OptionType { get; }

		string Name { get; }
		string Category { get; }
		string Description { get; }
		string Target { get; }

		Branch Branch { get; }
	}

	public class Composite<T, O> : IComposite{
		public string Name { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }

		public Branch Branch { get; set; }
		public string Target { get; set; }

		public Type SourceType => typeof(T);
		public Type OptionType => typeof(O);
	}
}
