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

		public override int GetHashCode() {
			if (ObjectType == null) {
				return 0;
			} else {
				return ObjectType.GetHashCode();
			}
		}

		public override bool Equals(object obj) {
			return ObjectType == ((SourceType)obj)?.ObjectType;
		}
	}
}
