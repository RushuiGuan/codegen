﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell
{
	[Cmdlet(VerbsCommon.Set, "AssemblyLocation")]
    public class SetAssemblyLocation : BaseCmdlet<IFactory<CodeGenSetting>> {

		[Parameter(Position = 0, ValueFromPipeline = true)]
		public DirectoryInfo[] Locations { get; set; } = new DirectoryInfo[0];

		protected override void ProcessRecord() {
			var setting = Handle.Get();
			setting.AssemblyLocations = (from item in Locations where item.Exists select item.FullName).Distinct();
			base.Factory.Create<ISaveFile<CodeGenSetting>>().Save(setting);
		}
	}
}
