using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer {
	public class WriteParameters : CodeGeneratorBase<IEnumerable<Parameter>> {
		ICodeGenerator<Parameter> writeParam;

		public WriteParameters(ICodeGenerator<Parameter> writeParam) {
			this.writeParam = writeParam;
		}

		public override void Run(TextWriter writer, IEnumerable<Parameter> source) {
			if (source != null) {
				foreach (var item in source) {
					writer.Run(writeParam, item);
					if (item != source.Last()) {
						writer.Comma().Space();
					}
				}
			}
		}
	}
}
