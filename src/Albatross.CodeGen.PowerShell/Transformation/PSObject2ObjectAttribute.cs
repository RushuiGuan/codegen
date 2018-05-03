using Albatross.CodeGen.Core;
using System;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell.Transformation {
	public class PSObject2ObjectAttribute : ArgumentTransformationAttribute {
		public override object Transform(EngineIntrinsics engineIntrinsics, object inputData) {
			if (inputData is PSObject) {
				inputData = ((PSObject)inputData).BaseObject;
			}
			return inputData;
		}
	}
}
