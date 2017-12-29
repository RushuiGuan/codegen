using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Albatross.CodeGen.Tool.View {
	public static class BindingsManager {
		public const string UpdateBindingWhenEnterPropertyName = "UpdateBindingWhenEnter";
		public static readonly DependencyProperty UpdateBindingWhenEnterProperty = DependencyProperty.RegisterAttached(UpdateBindingWhenEnterPropertyName, typeof(DependencyProperty), typeof(BindingsManager), new PropertyMetadata(UpdateBindingWhenEnterPropertyChangedCallback));
		public static void SetUpdateBindingWhenEnter(UIElement element, string value) {
			element.SetValue(UpdateBindingWhenEnterProperty, value);
		}
		public static string GetUpdateBindingWhenEnter(UIElement element) {
			return (string)element.GetValue(UpdateBindingWhenEnterProperty);
		}
		static void UpdateBindingWhenEnterPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs args) {
			UIElement elem = d as UIElement;
			if (elem != null) {
				if (args.OldValue != null) {
					elem.PreviewKeyDown -= Elem_PreviewKeyDown;
				}
				if (args.NewValue != null) {
					elem.PreviewKeyDown += Elem_PreviewKeyDown;
				}
			}
		}

		private static void Elem_PreviewKeyDown(object sender, KeyEventArgs e) {
			if (e.Key == Key.Enter) {
				UIElement elem = sender as UIElement;
				if (elem != null) {
					DependencyProperty property = elem.GetValue(UpdateBindingWhenEnterProperty) as DependencyProperty;
					if (property != null) {
						BindingOperations.GetBindingExpression(elem, property)?.UpdateSource();
					}
				}
			}
		}
	}
}
