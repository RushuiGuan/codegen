using Albatross.CodeGen.Shell.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Albatross.CodeGen.Shell.ViewModel {
	public class DefaultDataTemplateSelector : DataTemplateSelector {
		public DefaultDataTemplateSelector() {
		}

		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			Type type = item?.GetType();
			if (type == typeof(CodeGeneratorCollectionViewModel)) {
				return CreateTemplate<CodeGeneratorCollectionView>();
			} else if (type == typeof(CompositeDetailViewModel)) {
				return CreateTemplate<CompositeDetailView>();
			}
			return base.SelectTemplate(item, container);
		}

		DataTemplate CreateTemplate<T>() {
			Type viewType = typeof(T);
			string xaml = $"<DataTemplate> <v:{viewType.Name} /> </DataTemplate>";
			var context = new ParserContext();

			context.XamlTypeMapper = new XamlTypeMapper(new string[0]);
			context.XamlTypeMapper.AddMappingProcessingInstruction("v", viewType.Namespace, viewType.Assembly.FullName);

			context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
			context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
			context.XmlnsDictionary.Add("v", "v");

			var template = (DataTemplate)XamlReader.Parse(xaml, context);
			return template;
		}

	}
}
