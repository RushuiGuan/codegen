using Albatross.CodeGen.PowerShell.Transformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell
{
	[Cmdlet(VerbsCommon.Add, "CompositeLocation")]
    public class AddCompositeLocation : BaseCmdlet<IFactory<CodeGenSetting>> {

		[Parameter(Position =0, ValueFromPipeline =true)]
		[PathInfo2DirectoryInfoAttribute]
		public DirectoryInfo[] Locations { get; set; } = new DirectoryInfo[0];

		protected override void ProcessRecord() {
			var setting = Handle.Get();
			setting.CompositeLocations = (from item in Locations where item.Exists select item.FullName).Union(setting.CompositeLocations).Distinct();
			base.Factory.Create<ISaveFile<CodeGenSetting>>().Save(setting);
		}
	}
}
