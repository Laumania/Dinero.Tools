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
        public DifferenceEntryStates State { get; set; } = DifferenceEntryStates.Unbalanced;
        
        public DateTime RelevantDate
        {
            get { return DineroEntry?.Date ?? BankEntry.Date; }
        }
    }

    public enum DifferenceEntryStates
    {
        Unbalanced = 1,
        Balanced = 2,
        SelfCancelling = 3
    }
}
