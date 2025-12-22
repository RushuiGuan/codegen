using Albatross.CodeGen.CSharp;
using Albatross.CodeGen.CSharp.Declarations;
using Albatross.CodeGen.CSharp.Expressions;
using Albatross.CodeGen.WebClient.Models;
using System.Collections.Generic;

namespace Albatross.CodeGen.WebClient.CSharp {
	public class CreateHttpClientRegistrations {
		private readonly CSharpWebClientSettings settings;

		public CreateHttpClientRegistrations(CSharpWebClientSettings settings) {
			this.settings = settings;
		}

		public FileDeclaration Generate(IEnumerable<ControllerInfo> models) {
			return new FileDeclaration("Extensions.generated") {
				Namespace = new NamespaceExpression(settings.Namespace),
				NullableEnabled = true,
				Imports = [
					new ImportExpression("Microsoft.Extensions.DependencyInjection"),
				],
				Classes = [
					new ClassDeclaration {
						Name = new IdentifierNameExpression("Extensions"),
						AccessModifier = Defined.Keywords.Public,
						IsStatic = true,
						IsPartial = true,
						Methods = [
							new MethodDeclaration {
								Name = new IdentifierNameExpression("AddClients"),
								ReturnType = new TypeExpression("IHttpClientBuilder"),
								AccessModifier = Defined.Keywords.Public,
								IsStatic = true,
								Parameters = new ListOfParameterDeclarations(new ParameterDeclaration {
									Type = new TypeExpression("IHttpClientBuilder"),
									Name = new IdentifierNameExpression("builder"),
									UseThisKeyword = true
								}),
								Body = new ReturnExpression {
									Expression = new IdentifierNameExpression("builder")
										.Chain(true, GetRegistrationFunctions(settings.UseInterface, models))
								}
							}
						]
					}
				],
			};
		}

		IEnumerable<InvocationExpression> GetRegistrationFunctions(bool useInterface, IEnumerable<ControllerInfo> models) {
			foreach (var model in models) {
				var interfaceName = $"I{model.ControllerName}ProxyService";
				var className = $"{model.ControllerName}ProxyService";
				yield return new InvocationExpression {
					CallableExpression = new IdentifierNameExpression("AddTypedClient") {
						GenericArguments = useInterface
							? new ListOfGenericArguments(
								new TypeExpression(interfaceName),
								new TypeExpression(className))
							: new ListOfGenericArguments(
								new TypeExpression(className))
					}
				};
			}
		}
	}
}