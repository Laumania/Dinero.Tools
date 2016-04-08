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

            result.DifferenceEntryModels    = GetDirrenceEntryModel(dineroEntries, bankEntries);
            result.OriginalDineroEntries    = dineroEntries;
            result.OriginalBankEntries      = bankEntries;
            result.TotalBank                = bankEntries.OrderByDescending(x => x.Date).First().Saldo;
            result.TotalDinero              = dineroEntries.OrderByDescending(x => x.Date).First().Saldo;
            result.TotalDifference          = result.TotalDinero - result.TotalBank;

            return result;
        }

        private IEnumerable<DifferenceEntryModel> GetDirrenceEntryModel(IEnumerable<EntryModel> dineroEntries, IEnumerable<EntryModel> bankEntries)
        {
            var result = new List<DifferenceEntryModel>();
            foreach (var dineroEntry in dineroEntries)
            {
                var foundBankEntry = bankEntries.FirstOrDefault(x => x.Amount == dineroEntry.Amount && x.Dirty == false);
                if (foundBankEntry != null)
                {
                    result.Add(new DifferenceEntryModel()
                    {
                        BankEntry   = foundBankEntry,
                        DineroEntry = dineroEntry,
                        Status      = EntryStatus.Balanced
                    });
                    foundBankEntry.Dirty = true;
                }
                else
                {
                    result.Add(new DifferenceEntryModel()
                    {
                        BankEntry = null,
                        DineroEntry = dineroEntry,
                        Status = EntryStatus.Unbalanced
                    });
                }
                dineroEntry.Dirty = true;
            }
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
