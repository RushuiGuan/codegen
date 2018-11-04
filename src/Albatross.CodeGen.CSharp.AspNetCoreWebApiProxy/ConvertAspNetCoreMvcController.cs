using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc.Routing;
using Albatross.CodeGen.CSharp.Reflection;

namespace Albatross.CodeGen.CSharp.AspNetCoreWebApiProxy {
	/// <summary>
	/// Convert a type of AspNet Controller to the ControllerClass
	/// </summary>
	public class ConvertAspNetCoreMvcController : IConvertController {
		SetMethodByReflection setMethodByReflection;

		public ConvertAspNetCoreMvcController(SetMethodByReflection setMethodByReflection) {
			this.setMethodByReflection = setMethodByReflection;
		}
		public ControllerClass Convert(Type type) {
			if (typeof(Controller).IsAssignableFrom(type)) {

				RouteAttribute attribute = (from attrib in type.GetCustomAttributes<RouteAttribute>() orderby attrib.Order select attrib).FirstOrDefault();
				var controllerClass = new ControllerClass {
					Name = type.Name,
					Route = attribute?.Template,
					Methods = from item in type.GetMethods()
							  where item.IsPublic && item.GetCustomAttribute<HttpMethodAttribute>() != null
							  select ConvertMethod(item),
				};
				return controllerClass;
			} else {
				throw new ArgumentException();
			}
		}
		
		ControllerMethod ConvertMethod(MethodInfo method) {
			if (method.IsPublic) {
				HttpMethodAttribute httpMethodAttribute = method.GetCustomAttributes<HttpMethodAttribute>().OrderBy(args => args.Order).FirstOrDefault();
		
				if(httpMethodAttribute != null) {
					return new ControllerMethod {
						Route = httpMethodAttribute.Template,
						HttpAction = httpMethodAttribute.Name,
					};
				}
			}
			return null;
		}
	}
}
