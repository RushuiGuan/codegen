using Albatross.CodeGen.CSharp.Core;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class StoredProcedureToDapperClass : IStoredProcedureToClass {
		IConvertSqlTypeToDotNetType getDotNetType;

		public StoredProcedureToDapperClass(IConvertSqlTypeToDotNetType getDotNetType) {
			this.getDotNetType = getDotNetType;
		}

		public Class Get(Procedure procedure) {
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
							 select new Albatross.CodeGen.CSharp.Core.Variable(Extension.VariableName(sqlParam.Name)) {
								 Type = getDotNetType.Get(sqlParam.Type),
							 },
			};

			method.Body.AppendLine("DynamicParameters dynamicParameters = new DynamicParameters();");
			method.Body.Append("return new CommandDefinition(dbConnection,);");

			return method;
		}
	}
}
