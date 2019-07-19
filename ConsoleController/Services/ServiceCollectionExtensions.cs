using System;
using ConsoleController.Parsing;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleController.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConsoleController(this IServiceCollection services)
        {
            services.AddSingleton<IConsoleController, ConsoleController>();
            services.AddSingleton<ICommandParser, CommandParser>();
            
            services.Scan(scan => scan
                .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(classes => classes.AssignableTo<IConsoleCommand>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
            
            return services;
        }
    }
}