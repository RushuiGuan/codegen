using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Albatross.CodeGen.Shell {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
			Build();
		}

		void Build() {
			Container c = new Container();
			new SqlServer.Pack().RegisterServices(c);

			SettingRepository repo = new SettingRepository();
			c.RegisterSingleton<SettingRepository>(repo);
			Settings settings = repo.Get();
			if (settings.GeneratorAssemblyLocations != null) {
				foreach (var location in settings.GeneratorAssemblyLocations) {
				}
			}

			new Pack().RegisterServices(c);
		}
	}
}
