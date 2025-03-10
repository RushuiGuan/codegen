using Albatross.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Test.WithInterface.Proxy {
	public static partial class Extensions {
		public static IServiceCollection AddTestProxy(this IServiceCollection services) {
			services.AddConfig<TestProxyConfig>();
			services.AddHttpClient("test-with-interface-proxy").AddClients()
				.AddTypedClient<RedirectTestProxyService>()
				.AddTypedClient<AbsUrlRedirectTestProxyService>()
				.ConfigureHttpClient((provider, client) => {
					var config = provider.GetRequiredService<TestProxyConfig>();
					client.BaseAddress = new Uri(config.EndPoint);
				}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler {
					UseDefaultCredentials = true,
					AllowAutoRedirect = false,
				});
			return services;
		}
	}
}