using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.PowerShell.Transformation
{
	public class String2LeafAttribute : ArgumentTransformationAttribute {
		public override object Transform(EngineIntrinsics engineIntrinsics, object inputData) {
			if (inputData is PSObject) {
				inputData = ((PSObject)inputData).BaseObject;
			}

			if (inputData is string) {
				return new Leaf((string)inputData);
			} else {
				if (inputData is Array) {
					Array array = (Array)inputData;
					for (int i = 0; i < array.Length; i++) {
						object value = array.GetValue(i);
						value = Transform(null, value);
						array.SetValue(value, i);
					}
				}
				return inputData;
			}
		}
	}
}
