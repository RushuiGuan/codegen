using System;
using System.Text;
using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;

namespace Albatross.CodeGen.CSharp {
    public class RenderDotNetProperty : IRenderDotNetProperty{
        IRenderDotNetType renderDotNetType;

        public RenderDotNetProperty(IRenderDotNetType renderDotNetType) {
            this.renderDotNetType = renderDotNetType;    
        }

        public StringBuilder Render(StringBuilder sb, DotNetProperty property) {
            sb.Public().Space();
            if (string.IsNullOrEmpty(property.TypeOverride)){
                renderDotNetType.Render(sb, property.Type, false);
            } else {
                sb.Append(property.TypeOverride);
            }
            sb.Space().Proper(property.Name).Space().AppendLine("{ get; set; }");
            return sb;
        }
    }
}
