using Albatross.DbScripting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.DbScripting.SqlServer {
	public static class BuiltInColumns {
		public readonly static BuiltInColumn[] Items = new BuiltInColumn[] {
			new BuiltInStringColumn("createdBy"),
			new BuiltInStringColumn("modifiedBy"),
			new BuiltInDateTimeColumn("created"),
			new BuiltInDateTimeColumn("modified"),
		};
	}
}
