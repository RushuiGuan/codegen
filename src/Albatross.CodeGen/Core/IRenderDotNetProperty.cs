using System;
using System.Text;
using Albatross.CodeGen.Generation;

namespace Albatross.CodeGen.Core {
    public interface IRenderDotNetProperty {
        StringBuilder Render(StringBuilder stringBuilder, DotNetProperty property);
    }
}
