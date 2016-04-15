using System;
using System.Diagnostics;

namespace Dinero.Tools.Difference.Core.Models
{
    [DebuggerDisplay("Amount: {Amount}, Saldo: {Saldo}, Date: {Date}, Text: {Text}")]
    public class EntryModel
    {
        public string Text { get; set; }
        public decimal Amount { get; set; }
        public decimal Saldo { get; set; }
        public DateTime Date { get; set; }
        public EntryModelStates State { get; set; } = EntryModelStates.Unprocessed;
    }

    public enum EntryModelStates
    {
        Unprocessed = 1,
        Processed = 2
    }
}
