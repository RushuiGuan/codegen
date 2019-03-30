using Albatross.CodeGen.Core;
using Albatross.CodeGen.CSharp.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace Albatross.CodeGen.CSharp.Writer
{
	public class WriteDotNetType: CodeGeneratorBase<DotNetType> {
		public override void Run(TextWriter writer, DotNetType type) {
            writer.Append(type.Name);
            if (type.IsGeneric && type.GenericTypes?.Count() > 0)
            {
                writer.OpenAngleBracket();
                bool first = true;
                foreach (var genericType in type.GenericTypes)
                {
                    if (!first)
                    {
                        writer.Comma().Space();
                    }
                    else
                    {
                        first = false;
                    }
                    writer.Run(this, genericType);
                }
                writer.CloseAngleBracket();
            }
        }
	}
}
