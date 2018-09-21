using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class Composite {
		public Composite() :this(null, null){
		}

		public Composite(Type srcType, Type optType) {
			this.SourceType = srcType?? typeof(object);
			this.OptionType = optType ?? typeof(object);
		}

		public Type SourceType { get; private set; }
		public Type OptionType { get; private set; }
		public Branch Branch { get; set; }

		public string Name { get; set; }
		public string Category { get; set; }
		public string Target { get; set; }
		public string Description { get; set; }


		public CodeGenerator GetMeta() {
			Type type = typeof(CompositeCodeGenerator<,>);
			var meta = new CodeGenerator {
				Name = Name,
				Target = Target,
				Category = Category,
				Description = Description,
				GeneratorType = type.MakeGenericType(SourceType, OptionType),
				SourceType = SourceType,
				OptionType = OptionType,
				Data = Branch,
			};
			return meta;
		}
	}
}
