using Microsoft.Extensions.DependencyInjection;
#nullable enable
namespace Test.WithInterface.Client {
	public static partial class Extensions {
		public static IHttpClientBuilder AddClients(this IHttpClientBuilder builder) {
			return builder
				.AddTypedClient<IArrayParamTestClient, ArrayParamTestClient>()
				.AddTypedClient<ICancellationTokenTestClient, CancellationTokenTestClient>()
				.AddTypedClient<IControllerRouteTestClient, ControllerRouteTestClient>()
				.AddTypedClient<ICustomJsonSettingsClient, CustomJsonSettingsClient>()
				.AddTypedClient<IDuplicateNameTestClient, DuplicateNameTestClient>()
				.AddTypedClient<IFilteredMethodClient, FilteredMethodClient>()
				.AddTypedClient<IFromBodyParamTestClient, FromBodyParamTestClient>()
				.AddTypedClient<IFromHeaderParamTestClient, FromHeaderParamTestClient>()
				.AddTypedClient<IFromQueryParamTestClient, FromQueryParamTestClient>()
				.AddTypedClient<IFromRoutingParamTestClient, FromRoutingParamTestClient>()
				.AddTypedClient<IHttpMethodTestClient, HttpMethodTestClient>()
				.AddTypedClient<IInterfaceAndAbstractClassTestClient, InterfaceAndAbstractClassTestClient>()
				.AddTypedClient<INullableParamTestClient, NullableParamTestClient>()
				.AddTypedClient<INullableReturnTypeTestClient, NullableReturnTypeTestClient>()
				.AddTypedClient<IObsoleteClient, ObsoleteClient>()
				.AddTypedClient<IPartiallyObsoleteClient, PartiallyObsoleteClient>()
				.AddTypedClient<IOmittedConstructorClient, OmittedConstructorClient>()
				.AddTypedClient<IRequiredParamTestClient, RequiredParamTestClient>()
				.AddTypedClient<IRequiredReturnTypeTestClient, RequiredReturnTypeTestClient>();
		}
	}
}