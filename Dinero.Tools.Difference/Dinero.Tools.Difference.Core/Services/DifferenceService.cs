using System;
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

            MarkFutureEntries(rawDifferenceModels);
            MarkCounterEntries(rawDifferenceModels);

            return rawDifferenceModels;
        }

        private void MarkCounterEntries(IEnumerable<DifferenceEntryModel> rawDifferenceModels)
        {
            foreach (var differenceEntryModel in rawDifferenceModels)
            {
                if (differenceEntryModel.DineroEntry != null &&
                    differenceEntryModel.DineroEntry.Text.StartsWith("Modpostering af: "))
                {
                    var counterText         = differenceEntryModel.DineroEntry.Text.Replace("Modpostering af: ", "");
                    var counterAmount       = differenceEntryModel.DineroEntry.Amount*-1;
                    var counterEntry        = rawDifferenceModels.FirstOrDefault(x => x?.DineroEntry?.Text == counterText &&
                                                                                      x?.DineroEntry?.Amount == counterAmount && 
                                                                                      x.State == DifferenceEntryStates.Unbalanced);
                    if (counterEntry != null)
                    {
                        counterEntry.State          = DifferenceEntryStates.SelfCancelling;
                        differenceEntryModel.State  = DifferenceEntryStates.SelfCancelling;
                    }
                }
            }
        }

        private void MarkFutureEntries(IEnumerable<DifferenceEntryModel> rawDifferenceModels)
        {
            foreach (var differenceEntryModel in rawDifferenceModels)
            {
                if(differenceEntryModel.RelevantDate.Date > DateTime.Now.Date)
                    differenceEntryModel.State = DifferenceEntryStates.Future;
            }
        }

        private IEnumerable<DifferenceEntryModel> GetDifferenceEntryModels(IEnumerable<EntryModel> dineroEntries, IEnumerable<EntryModel> bankEntries)
        {
            var result                  = new List<DifferenceEntryModel>();
            var processedEntryModels    = new List<EntryModel>();

            foreach (var dineroEntry in dineroEntries)
            {
                var foundBankEntry              = FindBankEntryWithNearestDate(bankEntries.Except(processedEntryModels), dineroEntry);
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

        private EntryModel FindBankEntryWithNearestDate(IEnumerable<EntryModel> bankEntries, EntryModel dineroEntryToMatch)
        {
            var foundEntries                        = bankEntries.Where(x => x.Amount == dineroEntryToMatch.Amount);
            EntryModel nearestEntryFound            = null;
            double smallestTimeDifferenceInMinutes  = double.MaxValue;

            foreach (var foundEntry in foundEntries)
            {
                var differenceInMinutes = Math.Abs((dineroEntryToMatch.Date - foundEntry.Date).TotalMinutes);

                if (differenceInMinutes < smallestTimeDifferenceInMinutes)
                {
                    smallestTimeDifferenceInMinutes = differenceInMinutes;
                    nearestEntryFound               = foundEntry;
                }

            }

            return nearestEntryFound;
        }
    }
}
