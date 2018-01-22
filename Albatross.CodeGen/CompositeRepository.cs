using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen {
	public class CompositeRepository {
		CodeGenSettingRepository settingRepo;

		public CompositeRepository(CodeGenSettingRepository settingRepo) {

		}
		public string FileExtension => ".composite";
	}
}
