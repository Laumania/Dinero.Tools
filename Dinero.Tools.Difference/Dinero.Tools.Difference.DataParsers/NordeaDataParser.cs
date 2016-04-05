using System;
using System.Collections.Generic;
using Dinero.Tools.Difference.Core.Interfaces;
using Dinero.Tools.Difference.Core.Models;
using FileHelpers;

namespace Dinero.Tools.Difference.DataParsers
{
    public class NordeaDataParser : IDataParser
    {
        public IEnumerable<EntryModel> Parse(string data)
        {
            var engine      = new FileHelperEngine<NordeaEntry>();
            var parseResult = engine.ReadString(data);
            var result      = new List<EntryModel>();

            foreach (var nordeaEntry in parseResult)
            {
                var entryModel = new EntryModel()
                {
                    Amount  = nordeaEntry.Amount,
                    Date    = nordeaEntry.Date,
                    Text    = nordeaEntry.Text
                };
                result.Add(entryModel);
            }

            return result;
        }
    }

    [DelimitedRecord(";"), IgnoreFirst(1)]
    internal class NordeaEntry
    {
        [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
        public DateTime Date;
        public string Text;
        [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
        public DateTime InterestDate;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal Amount;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal Saldo;
    }
}
