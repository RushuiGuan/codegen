using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	[CodeGenerator("csharp.crud.delete.interface", GeneratorTarget.CSharp, Category = "DotNet CSharp", Description = "Generating C# CRUD Delete interface")]
	public class CRUDDeleteInterface : ClassInterfaceGenerator<object> {

		public CRUDDeleteInterface(ICustomCodeSectionStrategy customCodeSectionStrategy) : base(customCodeSectionStrategy) {
		}

		public override bool IsInterface => true;
		public override string GetName(object t, CSharpClassOption option) {
			return "IDelete" + option.Name;
		}
		public override void RenderBody(StringBuilder sb, object t, CSharpClassOption options) {
			sb.Tab(options.TabLevel).Void().Append("Delete").OpenParenthesis().Proper(options.Name).Space().ProperVariable(options.Name).CloseParenthesis().Terminate();
		}
	}
}
