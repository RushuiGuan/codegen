using Albatross.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
#nullable enable
namespace Test.Client {
	public partial class InterfaceAndAbstractClassTestClient {
		public InterfaceAndAbstractClassTestClient(HttpClient client) {
			this.client = client;
			this.jsonSerializerOptions = DefaultJsonSerializerOptions.Value;
		}
		public const string ControllerPath = "api/interface-abstract-class-test";
		private HttpClient client;
		private JsonSerializerOptions jsonSerializerOptions;
		public async Task SubmitByInterface(Test.Dto.Classes.ICommand command, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/interface-as-param");
			builder.AddQueryStringIfSet("command", command);
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task SubmitByAbstractClass(Test.Dto.Classes.AbstractClass command, CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/abstract-class-as-param");
			builder.AddQueryStringIfSet("command", command);
			using var requestMsg = builder.Build();
			await this.client.Send<string>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.ICommand> ReturnInterfaceAsync(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/return-interface-async");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.ICommand>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.ICommand> ReturnInterface(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/return-interface");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.ICommand>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.AbstractClass> ReturnAbstractClassAsync(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/return-abstract-class-async");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.AbstractClass>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
		public async Task<Test.Dto.Classes.AbstractClass> ReturnAbstractClass(CancellationToken cancellationToken) {
			var builder = new RequestBuilder()
				.WithMethod(HttpMethod.Post)
				.WithRelativeUrl($"{ControllerPath}/return-abstract-class");
			using var requestMsg = builder.Build();
			return await this.client.ExecuteOrThrow<Test.Dto.Classes.AbstractClass>(requestMsg, this.jsonSerializerOptions, cancellationToken);
		}
	}
}