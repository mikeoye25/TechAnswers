using CsvHelper.Configuration;
using System.Globalization;

namespace TechAnswers.Core.Models
{
    public sealed class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Id).Name(Constants.CsvHeaders.Id);
            Map(m => m.ServiceId).Name(Constants.CsvHeaders.ServiceId);
            Map(m => m.ClientId).Name(Constants.CsvHeaders.ClientId);
            Map(m => m.OperationCount).Name(Constants.CsvHeaders.OperationCount);
            Map(m => m.TransactionTimeStamp).Name(Constants.CsvHeaders.TransactionTimeStamp);
            Map(m => m.Service).Name(Constants.CsvHeaders.Service);
            Map(m => m.Status).Name(Constants.CsvHeaders.Status);
            Map(m => m.TransactionTime).Ignore();
        }
    }
}
