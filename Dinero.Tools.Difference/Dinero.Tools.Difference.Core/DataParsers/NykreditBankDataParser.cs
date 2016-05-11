using System;
using System.Collections.Generic;
using Dinero.Tools.Difference.Core.Interfaces;
using Dinero.Tools.Difference.Core.Models;
using FileHelpers;

namespace Dinero.Tools.Difference.Core.DataParsers
{
    public class NykreditBankDataParser : IDataParser
    {
        public IEnumerable<EntryModel> Parse(string data)
        {
            var engine = new FileHelperEngine<NykreditBankEntry>();
            var parseResult = engine.ReadString(data);
            var result = new List<EntryModel>();

            for (var index = 0; index < parseResult.Length; index++)
            {
                var nordeaEntry = parseResult[index];
                var entryModel  = new EntryModel()
                {
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

        [DelimitedRecord(";"), IgnoreFirst(2), IgnoreLast(1)]
        private class NykreditBankEntry
        {
            [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
            public DateTime Date;

            public string Text;

            [FieldConverter(ConverterKind.Decimal, ",")]
            public decimal Amount;

            public string Afstemt;

            [FieldConverter(ConverterKind.Decimal, ",")]
            public decimal Saldo;

            [FieldConverter(ConverterKind.Date, "dd-MM-yyyy")]
            public DateTime RenteDato;

            public string EndToEnd;
            public string Kreditorreference;
            public string Type;
            public string Adviseringer;
            public string Dummy1;
            public string Dummy2;
            public string Dummy3;
        }
    }
}
