using AdminPanel.Data;
using AdminPanel.Services.Customer;
using AdminPanel.Services.Inventory;
using AdminPanel.Services.Order;
using AdminPanel.Services.Product;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace AdminPanel.Web
{
    public class Startup
    {
       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

            services.AddDbContext<AdminDbContext>(opts =>
            {
                opts.EnableDetailedErrors();
                opts.UseNpgsql(Configuration.GetConnectionString("admin.dev"));
            });

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IInventoryService, InventoryService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder => builder
            .WithOrigins(
                "http://localhost:8080",
                "http://localhost:8081",
                "http://localhost:8082")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            );
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
