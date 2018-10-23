using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class Composite {

		public Branch Branch { get; set; }
		public string Name { get; set; }
		public string Category { get; set; }
		public string Target { get; set; }
		public string Description { get; set; }

		public CodeGenerator GetMeta() {
			var meta = new CodeGenerator {
				Name = Name,
				Target = Target,
				Category = Category,
				Description = Description,
				GeneratorType = typeof(CompositeCodeGenerator),
				Data = Branch,
			};
			return meta;
		}
	}
}
