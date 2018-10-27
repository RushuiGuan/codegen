using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;

namespace Albatross.CodeGen.Web {
	public class Startup {
		public void ConfigureServices(IServiceCollection services) {
			services.AddMvc();
			services.AddSwagger();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
			app.UseExceptionHandler(new ExceptionHandlerOptions { ExceptionHandler = new JsonExceptionMiddleware().Invoke });

			app.UseSwaggerUi3WithApiExplorer(settings => {
				settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
			});

			app.UseMvc();
		}
	}
}
