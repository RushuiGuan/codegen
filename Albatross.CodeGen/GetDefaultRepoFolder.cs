using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Albatross.CodeGen{
	//set the default repository folder to be the same folder as the Albatross.CodeGen assembly
	public class GetDefaultRepoFolder : IGetDefaultRepoFolder {
		public const string AlbatrossCodeGenFolder = "AlbatrossCodeGen";
		public string Get() {
			string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + AlbatrossCodeGenFolder;
			if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
			return path;
		}
	}
}
