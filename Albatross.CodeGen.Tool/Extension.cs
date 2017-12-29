using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Tool {
	public static class Extension {
		public static IViewLocator Register<VM, V>(this IViewLocator locator) {
			locator.Register(typeof(VM), typeof(V));
			return locator;
		}

		#region json repo
		public const string JsonRepoFolderName = "Albatross Code Generator";
		public const string JsonFileExtension = ".json";
		public static string InitRootFolder() {
			string root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\" + JsonRepoFolderName;
			if (!Directory.Exists(root)) {
				Directory.CreateDirectory(root);
			}
			return root;
		}
		#endregion
	}
}
