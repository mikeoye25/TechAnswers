using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using TechAnswers.Core;
using TechAnswers.Core.Interfaces;
using TechAnswers.Core.Repositories;
using TechAnswers.Data;
using TechAnswers.Data.Interfaces;
using TechAnswers.Data.Models;
using TechAnswers.Data.Repositories;
using TechAnswers.Services;

namespace TechAnswers.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<TransactionDatabaseSettings>(
               options =>
               {
                   options.ConnectionString = Configuration.GetValue<string>("TransactionDatabaseSettings:ConnectionString");
                   options.DatabaseName = Configuration.GetValue<string>("TransactionDatabaseSettings:DatabaseName");
                   options.TransactionsCollectionName = Configuration.GetValue<string>("TransactionDatabaseSettings:TransactionsCollectionName");
               });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IMongoClient, MongoClient>(
                _ => new MongoClient(Configuration.GetValue<string>("TransactionDatabaseSettings:ConnectionString")));
            services.AddScoped<ITransactionDatabaseSettings, TransactionDatabaseSettings>();
            services.AddTransient<ITransactionDbContext, TransactionDbContext>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Transaction", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
