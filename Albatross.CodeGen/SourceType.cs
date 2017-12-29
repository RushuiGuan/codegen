using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class SourceType {
		public Type ObjectType { get; set; }
		public string Description { get; set; }
		public string Title { get { return $"{ObjectType.Name} - {Description}"; } }
	}
}
