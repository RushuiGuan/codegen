using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Albatross.CodeGen.Core
{
    public interface IReadCustomCode {
		Dictionary<string, string> Read(string code);
    }
}
