using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Albatross.CodeGen.CSharp
{
	[CodeGenerator("csharp.crud.create", GeneratorTarget.CSharp, Category = "DotNet CSharp", Description = "Generating C# CRUD Create Implementation")]
	public class CRUDCreateImplementation : ClassInterfaceGenerator<CRUDOperation> {
		IConvertSqlDataType convertSqlDataType;
		IRenderDotNetType renderDotNetType;

		public CRUDCreateImplementation(ICustomCodeSectionStrategy customCodeSectionStrategy, IConvertSqlDataType convertSqlDataType, IRenderDotNetType renderDotNetType) : base(customCodeSectionStrategy) {
			this.convertSqlDataType = convertSqlDataType;
			this.renderDotNetType = renderDotNetType;
		}

		public override string GetName(CRUDOperation t, CSharpClassOption option) {
			StringBuilder sb = new StringBuilder();
			sb.Append("Create").Proper(t.Table.Name);
			return sb.ToString();
		}

		public override void RenderBody(StringBuilder sb, CRUDOperation crud, CSharpClassOption options) {
			int tabLevel = 0;
			sb.Tab(tabLevel).Public();
			Type type = convertSqlDataType.GetDotNetType(crud.Table.IdentityColumn.Type);
			renderDotNetType.Render(sb, type, false).Space().Append("Create").OpenParenthesis().Proper(crud.Table.Name).Space().ProperVariable(crud.Table.Name).CloseParenthesis().OpenScope();
			tabLevel++;
			customCodeSection.Write("create", tabLevel, sb);
			sb.AppendLine();
			sb.Tab(tabLevel).Append("using (var db = GetDatabaseConnection()) ").OpenScope();
			tabLevel++;
			sb.Tab(tabLevel).Append("var commandDefinition = new DataLayer.").Proper(crud.Procedure.Name).OpenParenthesis();
			var parameters = from p in crud.Procedure.Parameters
							 join c in crud.Table.Columns
							 on p.Name.Substring(1).ToLower() equals c.Name.ToLower()
							 select c.Name;

			foreach (var parameter in parameters) {
				if (options?.TypeCasts?.ContainsKey(parameter) == true) {
					sb.OpenParenthesis().Append(options.TypeCasts[parameter]).CloseParenthesis();
				}
				sb.ProperVariable(crud.Table.Name).Dot().Proper(parameter);
				if (parameter != parameters.Last()) {
					sb.Comma().Space();
				}
			}
			sb.CloseParenthesis().Terminate();

			sb.Tab(tabLevel).Append("return db.QueryFirst");
			renderDotNetType.Generic(sb, convertSqlDataType.GetDotNetType(crud.Table.IdentityColumn.Type));
			sb.OpenParenthesis().Append("commandDefinition.Definition").CloseParenthesis().Terminate();

			tabLevel--;
			sb.Tab(tabLevel).CloseScope();
			tabLevel--;
			sb.Tab(tabLevel).CloseScope();
		}
	}
}
