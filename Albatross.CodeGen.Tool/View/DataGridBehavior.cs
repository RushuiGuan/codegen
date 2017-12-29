using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Albatross.CodeGen.Tool.View {
	public class DataGridBehavior : Behavior<DataGrid> {
		protected override void OnAttached() {
			base.OnAttached();
			this.AssociatedObject.MouseDoubleClick += AssociatedObject_MouseDoubleClick;
		}
		protected override void OnDetaching() {
			this.AssociatedObject.MouseDoubleClick += AssociatedObject_MouseDoubleClick;
		}

		public const string DoubleClickCommandPropertyName = "DoubleClickCommand";
		public static readonly DependencyProperty DoubleClickCommandProperty = DependencyProperty.Register(DoubleClickCommandPropertyName, typeof(ICommand), typeof(DataGridBehavior), new PropertyMetadata());
		public ICommand DoubleClickCommand {
			get { return (ICommand)GetValue(DoubleClickCommandProperty); }
			set { SetValue(DoubleClickCommandProperty, value); }
		}
		private void AssociatedObject_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			var cmd = DoubleClickCommand;

			IInputElement element = e.MouseDevice.DirectlyOver;
			if (cmd != null && element != null && element is FrameworkElement) {
				DataGridCell cell = ((FrameworkElement)element).Parent as DataGridCell;
				if (cell != null) {
					cmd.Execute(cell.DataContext);
					e.Handled = true;
				}
			}
		}
	}
}
