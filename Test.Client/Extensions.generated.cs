using Microsoft.Extensions.DependencyInjection;
#nullable enable
namespace Test.Client {
	public static partial class Extensions {
		public static IHttpClientBuilder AddClients(this IHttpClientBuilder builder) {
			return builder
				.AddTypedClient<ArrayParamTestClient>()
				.AddTypedClient<CancellationTokenTestClient>()
				.AddTypedClient<ControllerRouteTestClient>()
				.AddTypedClient<CustomJsonSettingsClient>()
				.AddTypedClient<DuplicateNameTestClient>()
				.AddTypedClient<FilteredMethodClient>()
				.AddTypedClient<FromBodyParamTestClient>()
				.AddTypedClient<FromHeaderParamTestClient>()
				.AddTypedClient<FromQueryParamTestClient>()
				.AddTypedClient<FromRoutingParamTestClient>()
				.AddTypedClient<HttpMethodTestClient>()
				.AddTypedClient<InterfaceAndAbstractClassTestClient>()
				.AddTypedClient<NullableParamTestClient>()
				.AddTypedClient<NullableReturnTypeTestClient>()
				.AddTypedClient<ObsoleteClient>()
				.AddTypedClient<PartiallyObsoleteClient>()
				.AddTypedClient<OmittedConstructorClient>()
				.AddTypedClient<RequiredParamTestClient>()
				.AddTypedClient<RequiredReturnTypeTestClient>();
		}
	}
}