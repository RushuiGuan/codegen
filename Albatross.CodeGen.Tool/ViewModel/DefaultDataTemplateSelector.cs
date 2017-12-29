using Albatross.CodeGen.Tool.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Albatross.CodeGen.Tool.ViewModel {
	//this is not used. because TabItem vituralized its content
	public class DefaultDataTemplateSelector : DataTemplateSelector {
		public const string ViewLocatorKey = "ViewLocator";


		public override DataTemplate SelectTemplate(object item, DependencyObject container) {
			//try to retrieve ViewLocator from the container object!!!
			if (item != null) {
				IViewLocator locator = (container as FrameworkElement)?.FindResource(ViewLocatorKey) as IViewLocator;
				Type type = locator.GetView(item.GetType());
				if (type != null) {
					return CreateTemplate(type);
				}
			}
			return base.SelectTemplate(item, container);
		}

		DataTemplate CreateTemplate(Type viewType) {
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
