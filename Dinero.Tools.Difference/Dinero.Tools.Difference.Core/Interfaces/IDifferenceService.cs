using System.Collections.Generic;
using Dinero.Tools.Difference.Core.Models;

namespace Dinero.Tools.Difference.Core.Interfaces
{
    public interface IDifferenceService
    {
        IEnumerable<DifferenceEntryModel> FindDifferences(IEnumerable<EntryModel> dineroEntries, IEnumerable<EntryModel> bankEntries);
    }
}
