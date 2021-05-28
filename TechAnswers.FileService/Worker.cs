using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TechAnswers.Core.Extensions;
using TechAnswers.Core.Interfaces;
using TechAnswers.Data.Models;

namespace TechAnswers.FileService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> Logger;
        private readonly WorkerOptions Options;
        private readonly ITransactionService TransactionService;

        public Worker(ILogger<Worker> logger, WorkerOptions options, ITransactionService transactionService)
        {
            Logger = logger;
            Options = options;
            TransactionService = transactionService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            var records = TransactionService.ReadCsvFileToTransaction(Options.FileName);
            foreach (var record in records)
            {
                record.TransactionTime = record.TransactionTimeStamp.ToDateTime(Options.DateFormat);
                await TransactionService.CreateTransaction(record);
            }
            Logger.LogInformation("Worker ends at: {time}", DateTimeOffset.Now);
        }
    }
}
