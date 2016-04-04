using System.Collections.Generic;

namespace Dinero.Tools.Difference.Core.Models
{
    public class DifferenceResultModel
    {
        public IEnumerable<EntryDifferenceModel> DifferenceEntries { get; set; }
        public IEnumerable<EntryDifferenceModel> AllEntries { get; set; }
    }
}
