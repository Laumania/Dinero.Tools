using System;
using System.Diagnostics;

namespace Dinero.Tools.Difference.Core.Models
{
    [DebuggerDisplay("Index: {Index}, Amount: {Amount}, Saldo: {Saldo}, RelevantDate: {Date}, Text: {Text}")]
    public class EntryModel
    {
        /// <summary>
        /// Lowest index is the oldest entry, highest is the latest.
        /// </summary>
        public int Index { get; set; }
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
