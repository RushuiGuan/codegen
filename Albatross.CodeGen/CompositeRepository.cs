using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class CompositeRepository : DefaultFolderRepository<Composite> {
		public CompositeRepository(IGetDefaultRepoFolder getDefaultFolder) : base(getDefaultFolder) {
		}
		public override string FileExtension => ".composite";
	}
}
