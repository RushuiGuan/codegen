﻿using Albatross.CodeGen.CSharp.Core;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	public class GenerateClassFromDatabaseTable : IDatabaseTableToClass {
		IGetDotNetType getDotNetType;

		public GenerateClassFromDatabaseTable(IGetDotNetType getDotNetType) {
			this.getDotNetType = getDotNetType;
		}

		public Class Get(Table table) {
			Class @class = new Class {
				Name = table.Name.Proper(),
			};

			var columns = from item in table.Columns select new Property(item.Name.Proper()) {
				Type = getDotNetType.Get(item.Type),
				Modifier = AccessModifier.Public,
			};

			return @class;
		}
	}
}
