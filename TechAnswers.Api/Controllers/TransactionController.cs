using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechAnswers.Core.Interfaces;
using TechAnswers.Core.Models;
using TechAnswers.Core.ViewModels;

namespace TechAnswers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService TransactionService;
        public TransactionController(ITransactionService transactionService)
        {
            this.TransactionService = transactionService;
        }

        [HttpGet("getalltransactions")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAllTransactions()
        {
            var transactions = await TransactionService.Get();
            return Ok(transactions);
        }

        [HttpGet("getdailytransactionsperserviceid")]
        public async Task<ActionResult<IEnumerable<DailyTransactionsPerServiceId>>> GetDailyTransactionsPerServiceId(DateTime selectedDate)
        {
            var transactions = await TransactionService.GetDailyTransactionsPerServiceId(selectedDate);
            return Ok(transactions);
        }

        [HttpGet("getdailytransactionsperserviceidperday")]
        public async Task<ActionResult<IEnumerable<DailyTransactionsPerServiceIdPerDay>>> GetDailyTransactionsPerServiceIdPerDay()
        {
            var transactions = await TransactionService.GetDailyTransactionsPerServiceIdPerDay();
            return Ok(transactions);
        }

        [HttpGet("getmonthlytransactionsperserviceidperday")]
        public async Task<ActionResult<IEnumerable<MonthlyTransactionsPerServiceIdPerDay>>> GetMonthlyTransactionsPerServiceIdPerDay()
        {
            var transactions = await TransactionService.GetMonthlyTransactionsPerServiceIdPerDay();
            return Ok(transactions);
        }

        [HttpGet("gettransactionsperserviceidusingtimerange")]
        public async Task<ActionResult<IEnumerable<DailyTransactionsPerServiceIdPerDay>>> GetTransactionsPerServiceIdUsingTimeRange(DateTime startDate, DateTime endDate)
        {
            var transactions = await TransactionService.GetTransactionsPerServiceIdUsingTimeRange(startDate, endDate);
            return Ok(transactions);
        }

        [HttpGet("gettransactionsperserviceidperclientidusingtimerange")]
        public async Task<ActionResult<IEnumerable<DailyTransactionsPerServiceIdPerClientId>>> GetTransactionsPerServiceIdPerClientIdUsingTimeRange(DateTime startDate, DateTime endDate)
        {
            var transactions = await TransactionService.GetTransactionsPerServiceIdPerClientIdUsingTimeRange(startDate, endDate);
            return Ok(transactions);
        }
    }
}
