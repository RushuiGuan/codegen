using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.SqlServer
{
	[CodeGenerator("csharp.table.class", GeneratorTarget.CSharp, Category = GeneratorCategory.SQLServer, Description = "Generate a C# class from a sql server table")]
	public class Table2CSharpClass : CSharpClassGenerator<Table> {
		IConvertSqlDataType typeConverter;
		IRenderDotNetType renderDotNetType;

		public override string GetClassName(Table t, CSharpClassOption option) {
			return t.Name.Proper();
		}

		public Table2CSharpClass( IConvertSqlDataType getCSharpType, IRenderDotNetType renderDotNetType) {
			this.typeConverter = getCSharpType;
			this.renderDotNetType = renderDotNetType;
		}

		public override void RenderBody(StringBuilder sb, Table t, CSharpClassOption options) {
			foreach (var item in t.Columns) {
				sb.Tab(options.TabLevel).Append("public ");
				Type type = typeConverter.GetDotNetType(item.Type);
				renderDotNetType.Render(sb, type, item.IsNullable);
				sb.Space().Append(item.Name.Proper()).Space().AppendLine("{ get; set; }");
			}
		}
	}
}
