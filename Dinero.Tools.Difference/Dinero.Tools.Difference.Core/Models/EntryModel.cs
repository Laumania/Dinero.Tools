using System;
using System.Diagnostics;

namespace Dinero.Tools.Difference.Core.Models
{
    /// <summary>
    /// Represent the 'raw' entry from either Dinero or bank.
    /// </summary>
    [DebuggerDisplay("Index: {Index}, Amount: {Amount}, Saldo: {Saldo}, Date: {Date}, Text: {Text}")]
    public class EntryModel
    {
        /// <summary>
        /// Zero based index, lowest index is the oldest entry, highest is the latest.
        /// </summary>
        public int Index { get; set; }
        public string Text { get; set; }
        public decimal Amount { get; set; }
        public decimal Saldo { get; set; }
        public DateTime Date { get; set; }
    }
}
