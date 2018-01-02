using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albatross.Logging.Core;

namespace Albatross.CodeGen {
	public class ScenarioRepository : DefaultFolderRepository<Scenario> {
		public ScenarioRepository(ILogFactory logFactory, IGetDefaultRepoFolder getDefaultFolder) : base(logFactory, getDefaultFolder) {
		}

		public override string FileExtension => "*.scenario";
	}
}
