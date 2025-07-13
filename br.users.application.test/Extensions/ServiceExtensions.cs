using Microsoft.OpenApi.Models;
using System.Reflection;

namespace br.users.application.test.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                //options.IncludeXmlComments(xmlFilePath);
                //options.EnableAnnotations();

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "UserCx v1",
                    Description = "API para receber requisições sobre usuários",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(Path.Combine("wwwroot", "br.users.application.test.Api.xml"));
            });
        }
    }
}
