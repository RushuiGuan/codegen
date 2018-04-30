using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Albatross.Database;
using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;

namespace Albatross.CodeGen.SqlServer {
	/// <summary>
	/// Create a The following class
	/// <code>
	/// public class StoredProcedureName{
	///		CommandDefinition definition;
	///		public StoredProcedureName(type1 param1, type2 param2, type3 param3, IDbTransaction transaction = null){
	///			DynamicParameters dynamicParameters = new DynamicParameters();
	///			dynamicParameters.Add("param1", @param1, dbType:DbType.String);	
	///			dynamicParameters.Add("param2", @param2, dbType:DbType.String);	
	///			dynamicParameters.Add("param3", @param3, dbType:DbType.String);	
	///			definition = new CommandDefinition("spname", dynamicParameters, commandType:CommandType.StoredProcedure, transaction:transaction));
	///		}
	///		public CommandDefinition Get() => definition;
	///	}
	/// </code>
	/// </summary>
	[CodeGenerator("procedure command definition", GeneratorTarget.CSharp, Category = GeneratorCategory.SQLServer, Description = "Generate Dapper Command Definition for stored procedures")]
	public class ProcedureCommandDefinitionClass : CSharpClassGenerator<Procedure> {
		IConvertSqlDataType convertDataType;
		IRenderDotNetType renderDotNetType;

		public ProcedureCommandDefinitionClass(IConvertSqlDataType getCSharpType,  IRenderDotNetType renderDotNetType) {
			this.convertDataType = getCSharpType;
			this.renderDotNetType = renderDotNetType;
		}

		public override string GetClassName(Procedure t, CSharpClassOption option) {
			return t.Name;
		}

		public override void RenderConstructor(StringBuilder sb, Procedure t, CSharpClassOption options) {
			string className = GetClassName(t, options);
			sb.Tab(options.TabLevel).Public().Append(className).OpenParenthesis();
			foreach (var item in t.Parameters) {
				Type type = convertDataType.GetDotNetType(item.Type);
				renderDotNetType.Render(sb, type, true).Space().Append(item.Name).Comma().Space();
			}
			sb.Append("IDbTransaction transaction = null").CloseParenthesis().OpenScope();
			options.TabLevel++;
			sb.Tab(options.TabLevel).AppendLine("DynamicParameters dynamicParameters = new DynamicParameters();");
			foreach (var item in t.Parameters) {
				sb.Tab(options.TabLevel).Append("dynamicParameters.Add").OpenParenthesis().Literal(item.Name).Space().Comma().Space().Append("@").Append(item.Name).Comma().Space().Append("dbType:System.Data.DbType.").Append(convertDataType.GetDbType(item.Type)).CloseParenthesis().Terminate();
			}
			sb.Tab(options.TabLevel).Append($"definition = new CommandDefinition(\"[{t.Schema}].[{t.Name}]\", dynamicParameters, commandType:CommandType.StoredProcedure, transaction:transaction);").AppendLine();
			options.TabLevel--;
			sb.Tab(options.TabLevel).CloseScope();
		}

		public override void RenderBody(StringBuilder sb, Procedure t, CSharpClassOption options) {
			sb.Tab(options.TabLevel).AppendLine("CommandDefinition definition;");
			sb.Tab(options.TabLevel).AppendLine("public CommandDefinition Get() => definition;");
		}
	}
}
