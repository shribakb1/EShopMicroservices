﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectinString = configuration.GetConnectionString("Database");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectinString));

            return services;
        }
    }
}
