using System;
using System.Collections.Generic;
using Dinero.Tools.Difference.Core.Interfaces;
using Dinero.Tools.Difference.Core.Models;
using FileHelpers;

namespace Dinero.Tools.Difference.Core.DataParsers
{
    public class ArbejdernesLandsBankDataParser : IDataParser
    {
        public IEnumerable<EntryModel> Parse(string data)
        {
            var engine = new FileHelperEngine<ArbejdernesLandsBankEntry>();
            var parseResult = engine.ReadString(data);
            var result = new List<EntryModel>();

            for (var index = 0; index < parseResult.Length; index++)
            {
                var nordeaEntry = parseResult[index];
                var entryModel  = new EntryModel()
                {
                    Index       = index,
                    Amount      = nordeaEntry.Amount,
                    Date        = nordeaEntry.Date,
                    Text        = nordeaEntry.Text,
                    Saldo       = nordeaEntry.Saldo
                };
                result.Add(entryModel);
            }

            return result;
        }

        [DelimitedRecord(";")]
        private class ArbejdernesLandsBankEntry
        {
            [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
            public DateTime Date;

            [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
            public string Text;

            [FieldConverter(ConverterKind.Decimal, ",")]
            public decimal Amount;

            [FieldConverter(ConverterKind.Decimal, ",")]
            public decimal Saldo;

            [FieldQuoted('"', QuoteMode.AlwaysQuoted)]
            [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
            public DateTime DummyDate;
        }
    }
}
