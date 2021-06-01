using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paylocity.Payroll.ApiService.Filters;
using Paylocity.Payroll.Operation.Facades;
using Paylocity.Payroll.Operation.Factories;
using Paylocity.Payroll.Operation.Interfaces;
using Paylocity.Payroll.Operation.Models;
using Paylocity.Payroll.Operation.Repositories;

namespace Paylocity.Payroll.ApiService
{
    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)))
                .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EmployeeModelValidator>())
                .AddApplicationPart(typeof(Startup).Assembly);

            services.AddTransient<IPaycheckFacade, PaycheckFacade>();
            services.AddTransient<IPayrollConfigRepository, PayrollConfigRepository>();
            services.AddTransient<IPayrollOperationHandler<EmployeeModel, PaycheckModel>, PayrollCalculatorHandler>();

            services.AddSingleton<IDiscountStrategyFactory, DiscountStrategyFactory>();

            services.AddScoped<ExceptionFilter>();
            services.AddScoped<ModelStateValidate>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}