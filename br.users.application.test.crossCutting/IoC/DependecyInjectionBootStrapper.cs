using br.users.application.test.application.Services;
using br.users.application.test.domain.Entities.UserCx;
using br.users.application.test.domain.Interfaces.Messaging;
using br.users.application.test.domain.Interfaces.Repositories;
using br.users.application.test.domain.Interfaces.Services;
using br.users.application.test.messasing;
using br.users.application.test.repository.Databases;
using br.users.application.test.repository.Databases.Interfaces;
using br.users.application.test.repository.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace br.users.application.test.crossCutting.IoC
{
    public static class DependecyInjectionBootStrapper
    {
        public static void RegisterAllClasses(this IServiceCollection services, IConfiguration configuration)
        {

            RegisterDatabase(services);
            RegisterRepositories(services);
            RegisterServices(services);
            RegisterMessages(services);
        }

        private static void RegisterDatabase(IServiceCollection services)
        {
            services.AddScoped<IDbMySQLSession, DbMySQLSession>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserCxRepository, UserCxRepository>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasher<Users>, PasswordHasher<Users>>();
        }

        private static void RegisterMessages(IServiceCollection services) 
        {
            services.AddScoped<IMessageBusService, MessageBusService>();
        }
        

    }
}
