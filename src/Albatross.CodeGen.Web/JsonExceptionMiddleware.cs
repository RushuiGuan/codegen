using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Albatross.CodeGen.Web {
	public class JsonExceptionMiddleware {
		JsonSerializer serializer = new JsonSerializer();
		public JsonExceptionMiddleware() {
			serializer.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}

		public async Task Invoke(HttpContext context) {
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Response.ContentType = "application/json";
			Exception err = context.Features.Get<IExceptionHandlerFeature>()?.Error;
			if(err != null) {
				ErrorMessage msg = new ErrorMessage {
					Message = "An error has occurred",
					ExceptionType = err.GetType().FullName,
					StackTrace = err.StackTrace,
					ExceptionMessage = err.Message,
				};
				using(var writer = new StreamWriter(context.Response.Body)) {
					serializer.Serialize(writer, msg);
					await writer.FlushAsync().ConfigureAwait(false);
				}
			}
		}
	}
}
