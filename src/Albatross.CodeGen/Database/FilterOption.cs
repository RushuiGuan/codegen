using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.Database{
	[Flags]
    public enum FilterOption {
		None = 0,
		ByIdentityColumn = 1,
		ByPrimaryKey = 2,
		Custom = 4,
    }
}
