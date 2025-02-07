﻿

using BackEnd.Helpers;
using BackEnd.RN;

namespace BackEnd.Configuration
{
    public static class confRN
    {
        public static IServiceCollection AddconfRN(this IServiceCollection services)
        {

            RegisterBussinessRules(services);
            return services;
        }

        private static IServiceCollection RegisterBussinessRules(this IServiceCollection services)
        {
            // here come our Dependency Inyection
            services.AddTransient<PeliculaRN>();
            services.AddTransient<CategoriaRN>();
            services.AddTransient<PersonaRN>();
            services.AddTransient<UsuarioRN>();
            services.AddTransient<ClienteRN>();




            return services;

        }
    }
}
