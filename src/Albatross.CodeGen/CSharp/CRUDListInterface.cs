using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	[CodeGenerator("csharp.crud.list.interface", GeneratorTarget.CSharp, Category = "DotNet CSharp", Description = "Generating C# CRUD List interface")]
	public class CRUDListInterface : ClassInterfaceGenerator<object> {

		public CRUDListInterface(ICustomCodeSectionStrategy customCodeSectionStrategy) : base(customCodeSectionStrategy) {
		}

		public override bool IsInterface => true;
		public override string GetName(object t, CSharpClassOption option) {
			return "IList" + option.Name;
		}
		public override void RenderBody(StringBuilder sb, object t, CSharpClassOption options) {
			sb.Tab(TabLevel).Append("IEnumerable<").Proper(options.Name).Append(">").Space().Append("List").OpenParenthesis().CloseParenthesis().Terminate();
		}
	}
}
