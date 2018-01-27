using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class Step<T, O> {
		public T Source { get; set; }
		public O Options { get; set; }
		public string Generator { get; set; }
	}
}
