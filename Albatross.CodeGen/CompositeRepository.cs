using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Albatross.Logging.Core;

namespace Albatross.CodeGen {
	public class CompositeRepository : DefaultFolderRepository<Composite> {
		public CompositeRepository(ILogFactory logFactory, IGetDefaultRepoFolder getDefaultFolder) : base(logFactory, getDefaultFolder) {
		}
		public override string FileExtension => ".composite";
	}
}
