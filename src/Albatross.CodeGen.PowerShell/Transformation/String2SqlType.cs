using Albatross.CodeGen.Core;
using Albatross.CodeGen.SqlServer;
using System;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell.Transformation {
	public class String2SqlTypeAttribute : ArgumentTransformationAttribute {
		public override object Transform(EngineIntrinsics engineIntrinsics, object inputData) {
			if (inputData is PSObject) {
				inputData = ((PSObject)inputData).BaseObject;
			}
			if (inputData is string text) {
				return new ParseSqlType().Parse(text);
			} else {
				return inputData;
			}
		}
	}
}
