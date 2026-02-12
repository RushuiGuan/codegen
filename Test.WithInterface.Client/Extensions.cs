using Albatross.Config;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Test.WithInterface.Client {
	public static partial class Extensions {
		public static IServiceCollection AddTestProxy(this IServiceCollection services) {
			services.AddConfig<TestProxyConfig>();
			services.AddHttpClient("test-with-interface-client").AddClients()
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