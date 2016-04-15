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
            
            result.DifferenceEntryModels    = GetDifferenceEntryModels(dineroEntries, bankEntries);
            
            result.TotalBank                = bankEntries.OrderByDescending(x => x.Date).First().Saldo;
            result.TotalDinero              = dineroEntries.OrderByDescending(x => x.Date).First().Saldo;
            result.TotalDifference          = result.TotalDinero - result.TotalBank;

            return result;
        }

        private IEnumerable<DifferenceEntryModel> GetDifferenceEntryModels(IEnumerable<EntryModel> dineroEntries, IEnumerable<EntryModel> bankEntries)
        {
            var result = new List<DifferenceEntryModel>();
            foreach (var dineroEntry in dineroEntries)
            {
                var foundBankEntry = bankEntries.FirstOrDefault(x => x.Amount == dineroEntry.Amount && x.State == EntryModelStates.Unprocessed);
                if (foundBankEntry != null)
                {
                    result.Add(new DifferenceEntryModel()
                    {
                        BankEntry   = foundBankEntry,
                        DineroEntry = dineroEntry,
                        State      = DifferenceEntryStates.Balanced
                    });
                    foundBankEntry.State = EntryModelStates.Processed;
                }
                else
                {
                    result.Add(new DifferenceEntryModel()
                    {
                        BankEntry = null,
                        DineroEntry = dineroEntry,
                        State = DifferenceEntryStates.Unbalanced
                    });
                }
                dineroEntry.State = EntryModelStates.Processed;
            }

            //Add all bankentries not used
            foreach (var bankEntry in bankEntries.Where(x => x.State == EntryModelStates.Unprocessed))
            {
                result.Add(new DifferenceEntryModel()
                {
                    BankEntry = bankEntry,
                    DineroEntry = null,
                    State = DifferenceEntryStates.Unbalanced
                });
                bankEntry.State = EntryModelStates.Processed;
            }

            return result;
        }
    }
}
