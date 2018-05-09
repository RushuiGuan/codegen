using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Albatross.CodeGen.Core
{
    public interface ICustomCodeSection {
		string ApplyTo { get; }
		Dictionary<string, string> Read(string code);
		void Write(string name, StringBuilder sb);
	}
}
