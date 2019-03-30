using Albatross.CodeGen.CSharp.Conversion;
using Albatross.CodeGen.CSharp.Model;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp.Conversion {
	public class ConvertStoredProcedureToDapperClass : IConvertObject<Procedure, Class> {
		ConvertSqlTypeToDotNetType getDotNetType;

		public ConvertStoredProcedureToDapperClass(ConvertSqlTypeToDotNetType getDotNetType) {
			this.getDotNetType = getDotNetType;
		}

		public Class Convert(Procedure procedure) {
			Class @class = new Class {
				Name = procedure.Name.Proper(),
				AccessModifier = AccessModifier.Public,
				Imports = new string[] { "Dapper", "System.Data", },
				Dependencies = new Dependency[] {
					  new Dependency("dbConn") {
						   FieldType = DotNetType.IDbConnection_ClassName,
						   Type = DotNetType.IDbConnection_ClassName,
					  }
				 },
				Methods = new Method[] {
					GetCreateMethod(procedure),
				},
			};
			return @class;
		}

		Method GetCreateMethod(Procedure procedure) {
			Method method = new Method("Create") {
				ReturnType = new DotNetType("CommandDefinition"),
				Variables = from sqlParam
							 in procedure.Parameters
							 select new Albatross.CodeGen.CSharp.Model.Variable(Extension.VariableName(sqlParam.Name)) {
								 Type = getDotNetType.Convert(sqlParam.Type),
							 },
			};

			method.Body.AppendLine("DynamicParameters dynamicParameters = new DynamicParameters();");
			method.Body.Append("return new CommandDefinition(dbConnection,);");

			return method;
		}
	}
}
