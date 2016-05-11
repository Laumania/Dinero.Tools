using System;
using System.Collections.Generic;
using Dinero.Tools.Difference.Core.Interfaces;
using Dinero.Tools.Difference.Core.Models;
using FileHelpers;

namespace Dinero.Tools.Difference.Core.DataParsers
{
    public class DanskeBankDataParser : IDataParser
    {
        public IEnumerable<EntryModel> Parse(string data)
        {
            var engine = new FileHelperEngine<DanskeBankEntry>();
            var parseResult = engine.ReadString(data);
            var result = new List<EntryModel>();

            for (var index = 0; index < parseResult.Length; index++)
            {
                var nordeaEntry = parseResult[index];
                var entryModel  = new EntryModel()
                {
                    //Note: The sample DanskeBank file I got, has only entries from the same day.
                    //Therefore, I'm not currently aware which way the index goes.
                    Index       = parseResult.Length - (index + 1),
                    Amount      = nordeaEntry.Amount,
                    Date        = nordeaEntry.Date,
                    Text        = nordeaEntry.Text,
                    Saldo       = nordeaEntry.Saldo
                };
                result.Add(entryModel);
            }

            return result;
        }

        [DelimitedRecord(";"), IgnoreFirst(1), IgnoreEmptyLines()]
        private class DanskeBankEntry
        {
            [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
            [FieldConverter(ConverterKind.Date, "dd.MM.yyyy")]
            public DateTime Date;

            [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
            public string Text;

            [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
            [FieldConverter(ConverterKind.Decimal, ",")]
            public decimal Amount;

            [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
            [FieldConverter(ConverterKind.Decimal, ",")]
            public decimal Saldo;
        }
    }
}
