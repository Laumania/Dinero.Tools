using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineroKontoComparer.Core.Models
{
    public class DifferenceResultModel
    {
        public IEnumerable<EntryDifferenceModel> DifferenceEntries { get; set; }
        public IEnumerable<EntryDifferenceModel> AllEntries { get; set; }
    }
}
