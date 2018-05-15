using Albatross.CodeGen.Core;
using Albatross.CodeGen.Generation;
using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Albatross.CodeGen.CSharp {
	[CodeGenerator("asp.net webapi proxy", GeneratorTarget.CSharp, Category = "WebApi Proxy", Description = "Create a web api proxy class by using the reflection against the controller")]
	public class BuildWebApiProxy : ClassInterfaceGenerator<ObjectType> {

		const string ControllerPostfix = "Controller";
		IGetReflectionOnlyType getReflectionOnlyType;
		IRenderDotNetType renderDotNetType;

		public BuildWebApiProxy(IGetReflectionOnlyType getReflectionOnlyType, IRenderDotNetType renderDotNetType, ICustomCodeSectionStrategy strategy):base(strategy) {
			this.getReflectionOnlyType = getReflectionOnlyType;
			this.renderDotNetType = renderDotNetType;
		}

		public override string GetName(ObjectType objType, CSharpClassOption option) {
			return GetControllerName(getReflectionOnlyType.Get(objType)) + "ClientApi";
		}

		public string GetControllerName(Type classType) {
			string controller = classType.Name;
			if (controller.EndsWith(ControllerPostfix)) {
				controller = controller.Substring(0, controller.Length - ControllerPostfix.Length);
			}
			return controller;
		}
		const string HttpGetAttribName = "System.Web.Http.HttpGetAttribute";
		const string HttpDeleteAttribName = "System.Web.Http.HttpDeleteAttribute";
		const string HttpPostAttribName = "System.Web.Http.HttpPostAttribute";
		const string HttpPutAttribName = "System.Web.Http.HttpPutAttribute";


		public override void RenderBody(StringBuilder sb, ObjectType objType, CSharpClassOption options) {
			Type controllerType = getReflectionOnlyType.Get(objType);
			foreach (MethodInfo method in controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)) {
				foreach (CustomAttributeData data in method.GetCustomAttributesData()) {
					if (data.AttributeType.FullName == HttpGetAttribName) {
						BuildGetDelete(sb, method, "Get", controllerType);
					} else if (data.AttributeType.FullName == HttpDeleteAttribName) {
						BuildGetDelete(sb, method, "Delete", controllerType);
					} else if (data.AttributeType.FullName == HttpPostAttribName) {
						BuildPostPut(sb, method, "Post", controllerType);
					} else if (data.AttributeType.FullName == HttpPutAttribName) {
						BuildPostPut(sb, method, "Put", controllerType);
					}
				}
			}
		}


		public StringBuilder BuildGetDelete(StringBuilder sb, MethodInfo methodInfo, string method, Type controllerType) {
			string controller = GetControllerName(controllerType);
			sb.Tab(TabLevel).Public().Append("async ");
			if (methodInfo.ReturnType == typeof(void)) {
				sb.Append("Task");
			} else {
				sb.Generic(renderDotNetType, "Task", methodInfo.ReturnType);
			}
			sb.Space().Append(methodInfo.Name).OpenParenthesis();
			var parameters = methodInfo.GetParameters();
			foreach (var param in parameters) {
				renderDotNetType.Render(sb, param.ParameterType, false).Append(param.Name);
				if (param != parameters.Last()) {
					sb.Comma().Space();
				}
			}

			sb.CloseParenthesis().OpenScope();
			TabLevel++;
			sb.Tab(TabLevel).Append("string url = new StringBuilder().Action(").Literal(controller).Comma().Space().Literal(methodInfo.Name).CloseParenthesis();
			foreach (var param in parameters) {
				sb.Append(".BuildParam(").Literal(param.Name).Comma().Append(param.Name).CloseParenthesis();
			}
			sb.AsString().Terminate();
			sb.Tab(TabLevel).Append(@"var response = await HttpClient.").Append(method).Append("Async(url);").AppendLine();
			if (methodInfo.ReturnType.FullName == "System.Void") {
				sb.Tab(TabLevel).Append("await response.Handle()").Terminate();
			} else {
				sb.Tab(TabLevel).Return().Await().Variable("response").GenericMethod(renderDotNetType, "Handle", methodInfo.ReturnType).EmptyParenthesis().Terminate();
			}
			TabLevel--;
			return sb.Tab(TabLevel).CloseScope();
		}
		public StringBuilder BuildPostPut(StringBuilder sb, MethodInfo methodInfo, string method, Type controllerType) {
			string controller = GetControllerName(controllerType);
			sb.Tab(TabLevel).Public().Append("async ");
			if (methodInfo.ReturnType == typeof(void)) {
				sb.Append("Task");
			} else {
				sb.Generic(renderDotNetType, "Task", methodInfo.ReturnType);
			}
			sb.Space().Append(methodInfo.Name).OpenParenthesis();
			var param = methodInfo.GetParameters().FirstOrDefault();
			if (param != null) {
				renderDotNetType.Render(sb, param.ParameterType, false).Space().Append(param.Name);
			}
			sb.CloseParenthesis().OpenScope();
			TabLevel++;
			sb.Tab(TabLevel).Append("string url = new StringBuilder().Action(").Literal(controller).Comma().Space().Literal(methodInfo.Name).CloseParenthesis().AsString().Terminate();
			sb.Tab(TabLevel).Append(@"var response = await HttpClient.").Append(method);
			if (param != null) {
				sb.GenericMethod(renderDotNetType, "AsJsonAsync", param.ParameterType).OpenParenthesis().Append("url, ").Append(param.Name).CloseParenthesis().Terminate();
			} else {
				sb.Append("Async(url)").Terminate();
			}

			if (methodInfo.ReturnType.FullName == "System.Void") {
				sb.Tab(TabLevel).Append("await response.Handle()").Terminate();
			} else {
				sb.Tab(TabLevel).Append("return await response").GenericMethod(renderDotNetType, "Handle", methodInfo.ReturnType).EmptyParenthesis().Terminate();
			}
			TabLevel--;
			return sb.Tab(TabLevel).CloseScope();
		}
	}
}
