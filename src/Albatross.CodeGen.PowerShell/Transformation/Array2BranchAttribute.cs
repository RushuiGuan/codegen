using Albatross.CodeGen.Core;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Albatross.CodeGen.PowerShell.Transformation {
	public class Array2BranchAttribute : ArgumentTransformationAttribute {
		public override object Transform(EngineIntrinsics engineIntrinsics, object inputData) {
			if (inputData is PSObject) {
				inputData = ((PSObject)inputData).BaseObject;
			}

			if (inputData is Array array) {
				List<INode> list = new List<INode>();
				foreach (var item in array) {
					if (item is INode node) {
						list.Add(node);
					} else {
						list.Add(new Leaf(Convert.ToString(item)));
					}
				}
				return new Branch(list.ToArray());
			} else {
				return inputData;
			}
		}
	}
}
