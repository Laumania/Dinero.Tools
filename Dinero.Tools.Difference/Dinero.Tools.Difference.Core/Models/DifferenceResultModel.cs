using System.Collections.Generic;

namespace Dinero.Tools.Difference.Core.Models
{
    public class DifferenceResultModel
    {
        public IEnumerable<EntryDifferenceModel> DifferenceEntries { get; set; }

        public IEnumerable<EntryModel> OriginalBankEntries { get; set; }
        public IEnumerable<EntryModel> OriginalDineroEntries { get; set; }

        public decimal TotalBank { get; set; }
        public decimal TotalDinero { get; set; }
        public decimal TotalDifference { get; set; }
    }
}
