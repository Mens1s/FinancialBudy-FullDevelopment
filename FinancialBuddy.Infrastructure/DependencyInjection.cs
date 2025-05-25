using FinancialBuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FinancialBuddy.Application.Interfaces.Repositories;
using FinancialBuddy.Infrastructure.BackgroundJobs;

namespace FinancialBuddy.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FinancialBuddyDbContext>(
                options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            services.AddScoped<PaymentJob>();
            services.AddScoped<MockBankJob>();

            return services;
        }
    }
}
