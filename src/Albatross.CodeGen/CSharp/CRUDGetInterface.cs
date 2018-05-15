using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	[CodeGenerator("csharp.crud.get.interface", GeneratorTarget.CSharp, Category = "DotNet CSharp", Description = "Generating C# CRUD Get interface")]
	public class CRUDGetInterface : ClassInterfaceGenerator<object> {
		IRenderDotNetType renderDotNetType;

		public CRUDGetInterface(ICustomCodeSectionStrategy customCodeSectionStrategy, IRenderDotNetType renderDotNetType) : base(customCodeSectionStrategy) {
			this.renderDotNetType = renderDotNetType;
		}

		public override bool IsInterface => true;
		public override string GetName(object t, CSharpClassOption option) {
			return "IGet" + option.Name;
		}
		public override void RenderBody(StringBuilder sb, object t, CSharpClassOption options) {
			sb.Tab(TabLevel).Proper(options.Name).Space().Append("Get").OpenParenthesis();
			renderDotNetType.Render(sb, options.DefaultIdentityColumnType, false).Space().ProperVariable(options.Name).Append("ID").CloseParenthesis().Terminate();
		}
	}
}
