using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albatross.Logging.Core;

namespace Albatross.CodeGen.Tool {
	public class CompositeRepository : JsonFolderRepository<Composite> {
		public CompositeRepository(ILogFactory logFactory) : base(logFactory) {
		}
		public override string FolderName => "Composites";
	}
}
