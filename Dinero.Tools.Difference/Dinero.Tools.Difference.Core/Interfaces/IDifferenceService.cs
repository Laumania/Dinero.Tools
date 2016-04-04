using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DineroKontoComparer.Core.Models;

namespace DineroKontoComparer.Core.Interfaces
{
    public interface IDifferenceService
    {
        DifferenceResultModel FindDifferences(IEnumerable<EntryModel> dineroEntries, IEnumerable<EntryModel> bankEntries);
    }
}
