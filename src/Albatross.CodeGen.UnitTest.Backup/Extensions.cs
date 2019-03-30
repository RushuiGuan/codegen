using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Albatross.Test
{
    public static class Extensions
    {
		public static XmlDocument MakeXmlDoc(this string xml) {
			var doc = new XmlDocument();
			doc.LoadXml(xml);
			return doc;
		}
    }
}
