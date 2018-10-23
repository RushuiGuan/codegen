using System;
using System.Collections.Generic;
using System.Text;

namespace Albatross.CodeGen {
	public abstract class CodeGeneratorBase<SourceType, OptionType> : ICodeGenerator<SourceType, OptionType> {
#pragma warning disable 0067
		public event Func<StringBuilder, IEnumerable<object>> Yield;
#pragma warning restore 0067

		protected IEnumerable<object> InvokeYield(StringBuilder sb) {
			var result = Yield?.Invoke(sb);
			if (result == null) { result = new object[0]; }
			return result;
		}

		public abstract IEnumerable<object> Build(StringBuilder sb, SourceType source, OptionType option);

		public virtual void Configure(object data) { }

		public void ValidateSource(object source) {
			if (source != null && !(source is SourceType)) { throw new InvalidSourceException(); }
		}

		public virtual void ValidateOption(object option) {
			if (option != null && !(option is OptionType)) { throw new InvalidOptionException(); }
		}

		IEnumerable<object> ICodeGenerator.Build(StringBuilder sb, object source, object option) {
			ValidateSource(source);
			ValidateOption(option);
			return this.Build(sb, (SourceType)source, (OptionType)option);
		}
	}
}