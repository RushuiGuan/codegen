﻿using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Albatross.CodeGen.Database
{
	public interface IConvertDataType {
		Type GetDotNetType(SqlType sqlType);
		DbType GetDbType(SqlType sqlType);
	}
}
