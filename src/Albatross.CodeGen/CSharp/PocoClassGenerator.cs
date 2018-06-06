using Albatross.CodeGen.Core;
using Albatross.CodeGen.Database;
using Albatross.CodeGen.Generation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	[CodeGenerator("csharp.pococlass", GeneratorTarget.CSharp, Category = GeneratorCategory.SQLServer, Description = "Generate a C# class from a poco class object")]
    public class PocoClassGenerator : ICodeGenerator<PocoClass, CSharpClassOption> {
        IRenderDotNetProperty renderDotNetProperty;
        ICustomCodeSectionStrategy customCodeSectionStrategy;


        public int TabLevel { get; set; }
        public event Func<StringBuilder, IEnumerable<object>> Yield { add { } remove { } }


        public PocoClassGenerator(IRenderDotNetProperty renderDotNetProperty, ICustomCodeSectionStrategy customCodeSectionStrategy) {
            this.renderDotNetProperty = renderDotNetProperty;
            this.customCodeSectionStrategy = customCodeSectionStrategy;
		}

<<<<<<< HEAD
		public override void RenderBody(StringBuilder sb, PocoClass t, CSharpClassOption options) {
			foreach (var item in t.Properties) {
				sb.Tab(TabLevel).Append("public ");
				if (options?.PropertyTypeOverrides?.ContainsKey(item.Key) == true) {
					string type = options.PropertyTypeOverrides[item.Key];
					sb.Append(type);
				} else {
					renderDotNetType.Render(sb, item.Value, false);
				}
				sb.Space().Proper(item.Key).Space().AppendLine("{ get; set; }");
			}
=======
        public IEnumerable<object> Generate(StringBuilder sb, PocoClass source, CSharpClassOption option) {
            if (option.Imports != null) {
                foreach (var import in option.Imports) {
                    sb.AppendLine(import);
                    
                }
            }

            if (string.IsNullOrEmpty(source.Namespace)) {
                source.Namespace = option.Namespace;
            }
            bool hasNamespace = false;
            if (!string.IsNullOrEmpty(source.Namespace)) {
                sb.Append("namespace ").Append(source.Namespace).OpenScope();
                hasNamespace = true;
                TabLevel++;
            }
            ICustomCodeSection customCodeSection = customCodeSectionStrategy.Get(GeneratorTarget.CSharp);
            customCodeSection.Write("comment", TabLevel, sb);

            sb.Tab(TabLevel).PublicClass().Proper(source.Name).OpenScope();
            StringBuilder scoped_sb = new StringBuilder();
            foreach (var item in source.Properties) {
                renderDotNetProperty.Render(scoped_sb, item);
            }
            sb.Tabify(scoped_sb.ToString(), TabLevel);
            TabLevel--;
            sb.Tab(TabLevel).CloseScope();

            if (hasNamespace) {
                sb.CloseScope();
                TabLevel--;
            }
            return new object[] { this };
>>>>>>> 5d4b9fad7a72eb4d0df0e6cb36049d57ad7218df
		}


        public void Configure(object data) {
        }

        public IEnumerable<object> Generate(StringBuilder sb, object source, ICodeGeneratorOption option) {
            return this.ValidateNGenerate<PocoClass, CSharpClassOption>(sb, source, option);
        }
    }
}
