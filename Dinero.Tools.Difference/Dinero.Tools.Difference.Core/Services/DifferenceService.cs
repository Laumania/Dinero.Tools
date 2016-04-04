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
            var differenceEntries           = new List<EntryDifferenceModel>();

            result.OriginalDineroEntries    = dineroEntries;
            result.OriginalBankEntries      = bankEntries;
            result.TotalBank                = bankEntries.Sum(x => x.Amount);
            result.TotalDinero              = dineroEntries.Sum(x => x.Amount);
            result.TotalDifference          = result.TotalBank - result.TotalDinero;

            var dineroEntryAmountGroups = dineroEntries.GroupBy(x => x.Amount);
            var bankEntryAmountGroups   = bankEntries.GroupBy(x => x.Amount);

            foreach (var bankEntryAmountGroup in bankEntryAmountGroups)
            {
                var foundInDineroEntryAmountGroup = dineroEntryAmountGroups.FirstOrDefault(x => x.Key == bankEntryAmountGroup.Key);

                if (foundInDineroEntryAmountGroup?.Count() == bankEntryAmountGroup?.Count())
                {
                    //All is good
                }
                else
                {
                    //We have an error
                    var entryDifferenceModel = new EntryDifferenceModel {EntryModels = bankEntryAmountGroup.ToList()};
                    differenceEntries.Add(entryDifferenceModel);
                }
            }

            result.DifferenceEntries = differenceEntries;

            return result;
        }
    }
}
