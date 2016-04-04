using System.Collections.Generic;
using Dinero.Tools.Difference.Core.Models;

namespace Dinero.Tools.Difference.Core.Interfaces
{
    public interface IDifferenceService
    {
        DifferenceResultModel FindDifferences(IEnumerable<EntryModel> dineroEntries, IEnumerable<EntryModel> bankEntries);
    }
}
