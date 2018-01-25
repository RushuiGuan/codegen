using Albatross.CodeGen.Database;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell {
	[Cmdlet(VerbsCommon.Get, "DatabaseTable")]
	public class GetDatabaseTable : GetJsonObject<DatabaseObject>{
	}
}
