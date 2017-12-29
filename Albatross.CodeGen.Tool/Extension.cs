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


		#region dictionary extension methods
		public static IDictionary<K, IEnumerable<T>> Add<K, T>(this IDictionary<K, IEnumerable<T>> dict, K key, T t) {
			IEnumerable<T> list;
			if (!dict.TryGetValue(key, out list)) {
				list = new List<T>();
				dict.Add(key, list);
			}
			((List<T>)list).Add(t);
			return dict;
		}
		public static T Add<K, T>(this IDictionary<K, T> dict, K k, Func<T> func) {
			T t;
			if (!dict.TryGetValue(k, out t)) {
				t = func();
				dict.Add(k, t);
			}
			return t;
		}
		#endregion
	}
}
