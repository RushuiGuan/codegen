using Albatross.CodeGen;
using Albatross.CodeGen.TypeScript.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.TypeScript.Writer
{
    public class WriteInterface : IWriteObject<Interface>
    {
        WriteClassProperty writeProperty;
        public WriteInterface(WriteClassProperty writeProperty)
        {
            this.writeProperty = writeProperty;
        }

        public string Write(Interface t)
        {
            StringBuilder sb = new StringBuilder();

            if (t.Export) {
                sb.Append("export ");
            }
            sb.Append("interface ").Append(t.Name).Append("{");
            if (t.Properties != null)
            {
                foreach (var property in t.Properties)
                {
                    sb.AppendLine(this.writeProperty.Write(property));
                }
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
