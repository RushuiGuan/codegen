using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen
{
    public class ContextException:Exception
    {
		public ContextException(string key) :base($"Missing context with key: \"{key}\""){ }
		public ContextException(string key, Type type) : base($"Mismatch type found for context \"{key}\", expecting: {type.FullName}") { }
    }
}
