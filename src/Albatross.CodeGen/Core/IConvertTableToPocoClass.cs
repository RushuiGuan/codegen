using Albatross.CodeGen.Generation;
using Albatross.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Core
{
    public interface IConvertTableToPocoClass {
		PocoClass Convert(Table table, IDictionary<string, string> propertyTypeOverrides);
    }
}
