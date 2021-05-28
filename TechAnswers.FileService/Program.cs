using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using TechAnswers.Core;
using TechAnswers.Core.Interfaces;
using TechAnswers.Data;
using TechAnswers.Data.Interfaces;
using TechAnswers.Data.Models;
using TechAnswers.Services;

namespace TechAnswers.FileService
{
    public class Program
    {
        public IConfiguration Configuration { get; }
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    var transactionDatabaseSettings = configuration.GetSection("TransactionDatabaseSettings").Get<TransactionDatabaseSettings>();
                    var workerOptions = configuration.GetSection("WorkerOptions").Get<WorkerOptions>();
                    services.AddSingleton(workerOptions);
                    services.AddHostedService<Worker>();
                    services.Configure<TransactionDatabaseSettings>(
                       options =>
                       {
                           options.ConnectionString = transactionDatabaseSettings.ConnectionString;
                           options.DatabaseName = transactionDatabaseSettings.DatabaseName;
                           options.TransactionsCollectionName = transactionDatabaseSettings.TransactionsCollectionName;
                       });
                    services.AddSingleton<IUnitOfWork, UnitOfWork>();
                    services.AddSingleton<IMongoClient, MongoClient>(
                        _ => new MongoClient(hostContext.Configuration.GetValue<string>("TransactionDatabaseSettings:ConnectionString")));
                    services.AddTransient<ITransactionDbContext, TransactionDbContext>();
                    services.AddSingleton<ITransactionService, TransactionService>();
                });
    }
}
