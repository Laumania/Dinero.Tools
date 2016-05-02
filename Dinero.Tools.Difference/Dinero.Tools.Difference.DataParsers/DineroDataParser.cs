using System;
using System.Collections.Generic;
using Dinero.Tools.Difference.Core.Interfaces;
using Dinero.Tools.Difference.Core.Models;
using FileHelpers;

namespace Dinero.Tools.Difference.DataParsers
{
    public class DineroDataParser : IDataParser
    {
        public IEnumerable<EntryModel> Parse(string data)
        {
            var engine      = new FileHelperEngine<DineroEntry>();
            var parseResult = engine.ReadString(data);
            var result      = new List<EntryModel>();

            for (var index = 0; index < parseResult.Length; index++)
            {
                var dineroEntry = parseResult[index];
                var entryModel = new EntryModel()
                {
                    Index   = index,
                    Amount  = dineroEntry.Amount,
                    Date    = dineroEntry.Date,
                    Text    = dineroEntry.Text,
                    Saldo   = dineroEntry.Saldo
                };
                result.Add(entryModel);
            }

            return result;
        }
    }

    [DelimitedRecord(";"), IgnoreFirst(1)]
    internal class DineroEntry
    {
        public string Account;
        public string AccountName;
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime Date;
        public int? AppendixNumber;
        public string AppendixType;
        public string Text;
        public string TaxType;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal Amount;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal Saldo;
    }
}
