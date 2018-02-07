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
		 IEnumerable<string> Generators { get; }
		 string Target { get; }
	}

	public class Composite<T, O> : IComposite{
		public string Name { get; set; }
		public string Category { get; set; }
		public string Description { get; set; }

		public IEnumerable<string> Generators { get; set; }
		public string Target { get; set; }

		public Type SourceType => typeof(T);
		public Type OptionType => typeof(O);
	}
}
