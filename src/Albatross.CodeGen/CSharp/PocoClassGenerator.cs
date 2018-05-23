using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	[CodeGenerator("csharp.pococlass", GeneratorTarget.CSharp, Category = GeneratorCategory.SQLServer, Description = "Generate a C# class from a poco class object")]
	public class PocoClassGenerator : ClassInterfaceGenerator<PocoClass> {
		IRenderDotNetType renderDotNetType;

		public override string GetName(PocoClass t, CSharpClassOption option) {
			return t.Name;
		}

        public PocoClassGenerator(IRenderDotNetType renderDotNetType, ICustomCodeSectionStrategy strategy) : base(strategy) {
			this.renderDotNetType = renderDotNetType;
		}

		public override void RenderBody(StringBuilder sb, PocoClass t, CSharpClassOption options) {
			foreach (var item in t.Properties) {
				sb.Tab(TabLevel).Append("public ");
                if (options?.PropertyTypeOverrides?.ContainsKey(item.Key) == true) {
					string type = options.PropertyTypeOverrides[item.Key];
					sb.Append(type);
				} else {
                    renderDotNetType.Render(sb, item.Value, false);
				}
				sb.Space().Proper(item.Key).Space().AppendLine("{ get; set; }");
			}
		}
	}
}
