using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinero.Tools.Difference.Core.Models
{
    public class DifferenceEntryModel
    {
        public EntryModel DineroEntry { get; set; }
        public EntryModel BankEntry { get; set; }
        public EntryStatus Status { get; set; } = EntryStatus.Unbalanced;
    }
}
