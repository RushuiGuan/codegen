using Albatross.CodeGen.Core;
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
				if (options?.PropertyTypeOverrides?.ContainsKey(item.Name) == true) {
					string type = options.PropertyTypeOverrides[item.Name];
					sb.Append(type);
				} else {
					Type type = typeConverter.GetDotNetType(item.Type);
					renderDotNetType.Render(sb, type, item.IsNullable);
				}
				sb.Space().Proper(item.Name).Space().AppendLine("{ get; set; }");
			}
		}
	}
}
