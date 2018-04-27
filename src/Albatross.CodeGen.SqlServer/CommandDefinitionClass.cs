using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.Database;
using Albatross.Database;

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
	[CodeGenerator("sp_command_definition", GeneratorTarget.CSharp, Category = GeneratorCategory.SQLServer, Description = "Generate Dapper Command Definition for stored procedures")]
	public class CommandDefinitionClass : ClassGenerator<Procedure> {
		IGetCSharpType getCSharpType;

		public CommandDefinitionClass(IGetCSharpType getCSharpType) {
			this.getCSharpType = getCSharpType;
		}

		public override string GetClassName(Procedure t, ClassOption option) {
			return t.Name;
		}

		public override void RenderConstructor(StringBuilder sb, int tabLevel, Procedure t, ClassOption options) {
			string className = GetClassName(t, options);
			sb.Tab(tabLevel).Public().Append(className).OpenParenthesis();
			foreach (var item in t.Parameters) {
				Type type = getCSharpType.Get(item.Type);
				sb.Append(type.FullName).Space().Append(item.Name);
			}
			sb.Append("IDbTransaction transaction = null").CloseParenthesis().OpenScope();
			tabLevel++;
			sb.Tab(tabLevel).Append("DynamicParameters dynamicParameters = new DynamicParameters();");
			foreach (var item in t.Parameters) {
				sb.Tab(tabLevel).Append("dynamicParameters.Add").OpenParenthesis().Literal(item.Name).Space().Comma().Space().Append("@").Append(item.Name).CloseParenthesis().AppendLine();
				Type type = getCSharpType.Get(item.Type);
				sb.Append(type.FullName).Space().Append(item.Name);
			}
			sb.CloseScope();
		}

		public override void RenderBody(StringBuilder sb, int tabLevel, Procedure t, ClassOption options) {
			sb.Tab(tabLevel).AppendLine("public CommandDefinition Get() => definition;");
		}
	}
}
