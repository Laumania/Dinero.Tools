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
                var foundBankEntry  = bankEntries.FirstOrDefault(x => x.Amount == dineroEntry.Amount && x.State == EntryModelStates.Unprocessed);
                var diffEntryModel  = new DifferenceEntryModel()
                {
                    BankEntry   = foundBankEntry,
                    DineroEntry = dineroEntry
                };
                
                if (foundBankEntry != null)
                {
                    diffEntryModel.State = DifferenceEntryStates.Balanced;
                    foundBankEntry.State = EntryModelStates.Processed;
                }
                else
                {
                    //if (IsSelfCancelling(dineroEntry, dineroEntries))
                    //{
                    //    diffEntryModel.State = DifferenceEntryStates.SelfCancelling;
                    //}   
                    //else
                    {
                        diffEntryModel.State = DifferenceEntryStates.Unbalanced;
                    }
                }
                dineroEntry.State = EntryModelStates.Processed;
                result.Add(diffEntryModel);
            }

            AddAllUnprocessedBankEntries(result, bankEntries);

            return result;
        }

        private bool IsSelfCancelling(EntryModel dineroEntry, IEnumerable<EntryModel> dineroEntries)
        {
            var counterAmount = dineroEntry.Amount*-1;
            var foundEntryWithCounterAmount = dineroEntries.FirstOrDefault(x => x.State == EntryModelStates.Unprocessed && x.Amount == counterAmount);

            if (foundEntryWithCounterAmount != null)
            {
                //foundEntryWithCounterAmount.State = EntryModelStates.Processed;
                return true;
            }

            return false;
        }

        private void AddAllUnprocessedBankEntries(List<DifferenceEntryModel> differences, IEnumerable<EntryModel> bankEntries)
        {
            foreach (var bankEntry in bankEntries.Where(x => x.State == EntryModelStates.Unprocessed))
            {
                differences.Add(new DifferenceEntryModel()
                {
                    BankEntry = bankEntry,
                    DineroEntry = null,
                    State = DifferenceEntryStates.Unbalanced
                });
                bankEntry.State = EntryModelStates.Processed;
            }
        }
    }
}
