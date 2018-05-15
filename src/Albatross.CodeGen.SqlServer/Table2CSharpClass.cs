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
	public class Table2CSharpClass : ClassInterfaceGenerator<Table> {
		IConvertSqlDataType typeConverter;
		IRenderDotNetType renderDotNetType;

		public override string GetName(Table t, CSharpClassOption option) {
			return new StringBuilder().Proper(option.Name).ToString();
		}

		public Table2CSharpClass( IConvertSqlDataType getCSharpType, IRenderDotNetType renderDotNetType, ICustomCodeSectionStrategy strategy):base(strategy) {
			this.typeConverter = getCSharpType;
			this.renderDotNetType = renderDotNetType;
		}

		public override void RenderBody(StringBuilder sb, Table t, CSharpClassOption options) {
			foreach (var item in t.Columns) {
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
