﻿using System;
using System.Collections.Generic;
using System.Linq;
using Dinero.Tools.Difference.Core.Interfaces;
using Dinero.Tools.Difference.Core.Models;

namespace Dinero.Tools.Difference.Core.Services
{
    public class DifferenceService : IDifferenceService
    {
        public IEnumerable<DifferenceEntryModel> FindDifferences(IEnumerable<EntryModel> dineroEntries, IEnumerable<EntryModel> bankEntries)
        {
            var rawDifferenceModels = GetDifferenceEntryModels(dineroEntries, bankEntries);

            return rawDifferenceModels;
        }

        private IEnumerable<DifferenceEntryModel> GetDifferenceEntryModels(IEnumerable<EntryModel> dineroEntries, IEnumerable<EntryModel> bankEntries)
        {
            var result                  = new List<DifferenceEntryModel>();
            var processedEntryModels    = new List<EntryModel>();

            foreach (var dineroEntry in dineroEntries)
            {
                var foundBankEntry              = bankEntries.FirstOrDefault(x => x.Amount == dineroEntry.Amount && processedEntryModels.Contains(x) == false);
                var diffEntryModel              = new DifferenceEntryModel()
                {
                    DineroEntry                 = dineroEntry
                };
                
                if (foundBankEntry != null)
                {
                    diffEntryModel.BankEntry    = foundBankEntry;
                    diffEntryModel.State        = DifferenceEntryStates.Balanced;

                    processedEntryModels.Add(foundBankEntry);
                }
                else
                {
                    diffEntryModel.BankEntry    = null;
                    diffEntryModel.State        = DifferenceEntryStates.Unbalanced;
                }

                processedEntryModels.Add(dineroEntry);
                result.Add(diffEntryModel);
            }

            var unprocessedBankEntries          = bankEntries.Except(processedEntryModels);
            foreach (var bankEntry in unprocessedBankEntries)
            {
                result.Add(new DifferenceEntryModel()
                {
                    BankEntry = bankEntry,
                    DineroEntry = null,
                    State = DifferenceEntryStates.Unbalanced
                });
            }

            return result;
        }

        //private bool IsSelfCancelling(EntryModel dineroEntry, IEnumerable<EntryModel> dineroEntries)
        //{
        //    var counterAmount = dineroEntry.Amount*-1;
        //    var foundEntryWithCounterAmount = dineroEntries.FirstOrDefault(x => x.State == EntryModelStates.Unprocessed && x.Amount == counterAmount);

        //    if (foundEntryWithCounterAmount != null)
        //    {
        //        //foundEntryWithCounterAmount.State = EntryModelStates.Processed;
        //        return true;
        //    }

        //    return false;
        //}
    }
}
