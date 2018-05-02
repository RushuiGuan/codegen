using Albatross.CodeGen.Core;
using Albatross.CodeGen.PowerShell.Transformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell
{
	[Cmdlet(VerbsCommon.New, "Branch")]
    public class NewBranch : Cmdlet   {

		[Parameter(Mandatory =true, Position =0, ValueFromPipeline =true)]
		public INode[] Nodes{ get; set; }

		protected override void ProcessRecord() {
			WriteObject(new Branch(Nodes));
		}
	}
}
