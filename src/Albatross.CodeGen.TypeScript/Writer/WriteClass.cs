using Albatross.CodeGen;
using Albatross.CodeGen.TypeScript.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.TypeScript.Writer
{
    public class WriteClass : IWriteObject<Class>
    {
        WriteClassProperty writeProperty;
        public WriteClass(WriteClassProperty writeProperty)
        {
            this.writeProperty = writeProperty;
        }

        public string Write(Class t)
        {
            StringBuilder sb = new StringBuilder();

            if (t.Export) {
                sb.Append("export ");
            }
            sb.Append("class ").Append(t.Name).Append("{");
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
