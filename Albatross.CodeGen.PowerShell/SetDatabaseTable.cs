using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.Set, "DatabaseTable", SupportsShouldProcess = true)]
	public class SetDatabaseTable : SetJsonObject<Albatross.CodeGen.Database.DatabaseObject> { }
}
