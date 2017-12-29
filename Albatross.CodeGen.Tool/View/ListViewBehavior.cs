using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Albatross.CodeGen.Tool.View {
	public class ListViewBehavior : Behavior<ListView> {
		protected override void OnAttached() {
			base.OnAttached();
			this.AssociatedObject.MouseDoubleClick += AssociatedObject_MouseDoubleClick;
		}
		protected override void OnDetaching() {
			this.AssociatedObject.MouseDoubleClick -= AssociatedObject_MouseDoubleClick;
		}

		public const string DoubleClickCommandPropertyName = "DoubleClickCommand";
		public static readonly DependencyProperty DoubleClickCommandProperty = DependencyProperty.Register(DoubleClickCommandPropertyName, typeof(ICommand), typeof(ListViewBehavior), new PropertyMetadata());
		public ICommand DoubleClickCommand {
			get { return (ICommand)GetValue(DoubleClickCommandProperty); }
			set { SetValue(DoubleClickCommandProperty, value); }
		}
		private void AssociatedObject_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			ListView listView = sender as ListView;
			if (listView != null && DoubleClickCommand != null) {
				var cmd = DoubleClickCommand;

				var item = VisualTreeHelper.HitTest(listView, e.GetPosition(listView)).VisualHit;
				while (item != null) {
					if (item is ListViewItem) {
						cmd.Execute(((ListViewItem)item).DataContext);
						break;
					} else {
						item = VisualTreeHelper.GetParent(item);
					}
				}
			}
		}
	}
}
