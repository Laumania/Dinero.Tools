using System;
using System.Collections.Generic;
using System.Linq;
using Dinero.Tools.Difference.Core.Interfaces;
using Dinero.Tools.Difference.Core.Models;

namespace Dinero.Tools.Difference.Core.Services
{
    public class DifferenceService : IDifferenceService
    {
        public DifferenceResultModel FindDifferences(IEnumerable<EntryModel> dineroEntries, IEnumerable<EntryModel> bankEntries)
        {
            var result                      = new DifferenceResultModel();

            CalculateAndSetEntryStatus(dineroEntries, bankEntries);

            result.OriginalDineroEntries    = dineroEntries;
            result.OriginalBankEntries      = bankEntries;
            result.TotalBank                = bankEntries.Max(x => x.Saldo);
            result.TotalDinero              = dineroEntries.Max(x => x.Saldo);
            result.TotalDifference          = result.TotalDinero - result.TotalBank;

            return result;
        }

        private void CalculateAndSetEntryStatus(IEnumerable<EntryModel> dineroEntries, IEnumerable<EntryModel> bankEntries)
        {
            foreach (var bankEntry in bankEntries)
            {
                //Find all Dinero entries that match the amount of current bank entry, that isn't "Balanced" yet.
                var foundDineroEntry = dineroEntries.FirstOrDefault(x => x.Amount == bankEntry.Amount && x.Status == EntryStatus.Unbalanced);
                
                if (foundDineroEntry != null)
                {
                    bankEntry.Status = EntryStatus.Balanced;
                    foundDineroEntry.Status = EntryStatus.Balanced;
                }
            }
        }
    }
}
