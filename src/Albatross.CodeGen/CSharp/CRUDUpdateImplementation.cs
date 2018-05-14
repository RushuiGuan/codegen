﻿using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Albatross.CodeGen.CSharp
{
	[CodeGenerator("csharp.crud.update", GeneratorTarget.CSharp, Category = "DotNet CSharp", Description = "Generating C# CRUD Update Implementation")]
	public class CRUDUpdateImplementation : ClassInterfaceGenerator<CRUDOperation> {
		IConvertSqlDataType convertSqlDataType;
		IRenderDotNetType renderDotNetType;

		public CRUDUpdateImplementation(ICustomCodeSectionStrategy customCodeSectionStrategy, IConvertSqlDataType convertSqlDataType, IRenderDotNetType renderDotNetType) : base(customCodeSectionStrategy) {
			this.convertSqlDataType = convertSqlDataType;
			this.renderDotNetType = renderDotNetType;
		}

		public override string GetName(CRUDOperation t, CSharpClassOption option) {
			StringBuilder sb = new StringBuilder();
			sb.Append("Update").Proper(t.Table.Name);
			return sb.ToString();
		}

		public override void RenderBody(StringBuilder sb, CRUDOperation crud, CSharpClassOption options) {
			sb.Tab(options.TabLevel).Public().Void().Append("Update").OpenParenthesis().Proper(crud.Table.Name).Space().ProperVariable(crud.Table.Name).CloseParenthesis().OpenScope();
			options.TabLevel++;
			customCodeSection.Write("Update", options.TabLevel, sb);
			sb.AppendLine();
			sb.Tab(options.TabLevel).Append("using (var db = GetDatabaseConnection()) ").OpenScope();
			options.TabLevel++;
			sb.Tab(options.TabLevel).Append("var commandDefinition = new DataLayer.").Proper(crud.Procedure.Name).OpenParenthesis();
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

			sb.Tab(options.TabLevel).Append("db.Execute");
			sb.OpenParenthesis().Append("commandDefinition.Definition").CloseParenthesis().Terminate();

			options.TabLevel--;
			sb.Tab(options.TabLevel).CloseScope();
			options.TabLevel--;
			sb.Tab(options.TabLevel).CloseScope();
		}
	}
}
