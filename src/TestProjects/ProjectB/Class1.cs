using System;
using Newtonsoft.Json.Linq;

namespace ProjectB {
    public class Class1 {
        public Class1() {
            ProjectA.Class1 class1 = new ProjectA.Class1();
        }


        public string Name { get; set; }
        public ProjectA.Class1 ProjectA { get; set; }
        public JObject JObject { get; set; }
        public JObject B { get; set; }

    }
}
