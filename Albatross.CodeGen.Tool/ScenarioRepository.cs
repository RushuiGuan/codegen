using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albatross.Logging.Core;

namespace Albatross.CodeGen.Tool {
	public class ScenarioRepository : JsonFolderRepository<Scenario> {
		public ScenarioRepository(ILogFactory logFactory) : base(logFactory) {
		}
		public override string FolderName => "Scenarios";
	}
}
