using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	[CodeGenerator("csharp.crud.update.interface", GeneratorTarget.CSharp, Category = "DotNet CSharp", Description = "Generating C# CRUD Update interface")]
	public class CRUDUpdateInterface : ClassInterfaceGenerator<object> {
		IRenderDotNetType renderDotNetType;

		public CRUDUpdateInterface(ICustomCodeSectionStrategy customCodeSectionStrategy, IRenderDotNetType renderDotNetType) : base(customCodeSectionStrategy) {
			this.renderDotNetType = renderDotNetType;
		}

		public override bool IsInterface => true;
		public override string GetName(object t, CSharpClassOption option) {
			return "IUpdate" + option.Name;
		}
		public override void RenderBody(StringBuilder sb, object t, CSharpClassOption options) {
			sb.Tab(TabLevel).Void().Append("Update").OpenParenthesis().Proper(options.Name).Space().ProperVariable(options.Name).CloseParenthesis().Terminate();
		}
	}
}
