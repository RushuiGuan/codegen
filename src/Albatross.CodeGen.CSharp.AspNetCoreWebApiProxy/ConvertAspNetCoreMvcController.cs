using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Albatross.CodeGen.CSharp.AspNetCoreWebApiProxy {
	/// <summary>
	/// Convert a type of AspNet Controller to the ControllerClass
	/// </summary>
	public class ConvertAspNetCoreMvcController : IConvertController {
		public ControllerClass Convert(Type type) {
			if (typeof(Controller).IsAssignableFrom(type)) {

				RouteAttribute attribute = (from attrib in type.GetCustomAttributes<RouteAttribute>() orderby attrib.Order select attrib).FirstOrDefault();
				var controllerClass = new ControllerClass {
					Name = type.Name,
					Route = attribute?.Template,
				};

				foreach(var method in type.GetMethods()) {
					if (method.IsPublic) {
						HttpMethodAttribute httpMethod = GetHttpMethod(method);
						if(httpMethod != null) {
							ControllerMethod controllerMethod = new ControllerMethod {
								Route = httpMethod.Template,
								HttpAction = httpMethod.Name,
								Name = method.Name,
							};
						}
					}
				}


				return controllerClass;
			} else {
				throw new ArgumentException();
			}
		}
		HttpMethodAttribute GetHttpMethod(MethodInfo method) {
			HttpMethodAttribute httpMethodAttribute = (from attrib in method.GetCustomAttributes<HttpGetAttribute>() orderby attrib.Order select attrib).FirstOrDefault();
			if (httpMethodAttribute == null) {
				httpMethodAttribute = (from attrib in method.GetCustomAttributes<HttpPostAttribute>() orderby attrib.Order select attrib).FirstOrDefault();
				if (httpMethodAttribute == null) {
					httpMethodAttribute = (from attrib in method.GetCustomAttributes<HttpPutAttribute>() orderby attrib.Order select attrib).FirstOrDefault();
					if (httpMethodAttribute == null) {
						httpMethodAttribute = (from attrib in method.GetCustomAttributes<HttpDeleteAttribute>() orderby attrib.Order select attrib).FirstOrDefault();
					}
				}
			}
			return httpMethodAttribute;
		}
	}
}
