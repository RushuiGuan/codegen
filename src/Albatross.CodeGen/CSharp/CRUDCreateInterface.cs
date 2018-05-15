using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	[CodeGenerator("csharp.crud.create.interface", GeneratorTarget.CSharp, Category = "DotNet CSharp", Description = "Generating C# CRUD Create interface")]
	public class CRUDCreateInterface : ClassInterfaceGenerator<object> {
		IRenderDotNetType renderDotNetType;

		public CRUDCreateInterface(ICustomCodeSectionStrategy customCodeSectionStrategy, IRenderDotNetType renderDotNetType) : base(customCodeSectionStrategy) {
			this.renderDotNetType = renderDotNetType;
		}

		public override bool IsInterface => true;
		public override string GetName(object t, CSharpClassOption option) {
			return "ICreate" + option.Name;
		}
		public override void RenderBody(StringBuilder sb, object t, CSharpClassOption options) {
			sb.Tab(TabLevel);
			renderDotNetType.Render(sb, options.DefaultIdentityColumnType, false).Space().Append("Create").OpenParenthesis().Proper(options.Name).Space().ProperVariable(options.Name).CloseParenthesis().Terminate();
		}
	}
}
