using System;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;

namespace Albatross.CodeGen.CSharp {
	[CodeGenerator("csharp.class.test", GeneratorTarget.CSharp, Category = "DotNet CSharp", Description = "Generating test C# class")]
	public class TestCSharpClass : CSharpClassGenerator<object> {
		IRenderDotNetType renderDotNetType;

		public TestCSharpClass(IRenderDotNetType renderDotNetType, ICustomCodeSectionStrategy strategy): base(strategy) {
			this.renderDotNetType = renderDotNetType;
		}

		public override string GetClassName(object t, CSharpClassOption option) {
			return "Test";
		}
		public override void RenderBody(StringBuilder sb, object t, CSharpClassOption options) {
			sb.Tab(options.TabLevel).PublicGetSet(renderDotNetType, typeof(string), "Name");
		}
	}
}
