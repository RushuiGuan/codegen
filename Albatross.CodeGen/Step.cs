using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class Step{
		public object Source { get; set; }
		public object Options { get; set; }
		public string Generator { get; set; }
	}
}
