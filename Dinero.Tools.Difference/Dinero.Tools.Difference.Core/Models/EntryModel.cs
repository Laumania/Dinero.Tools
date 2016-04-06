using System;
using System.Diagnostics;

namespace Dinero.Tools.Difference.Core.Models
{
    [DebuggerDisplay("Status: {Status}, Amount: {Amount}, Saldo: {Saldo}, Date: {Date}, Text: {Text}")]
    public class EntryModel
    {
        public string Text { get; set; }
        public decimal Amount { get; set; }
        public decimal Saldo { get; set; }
        public DateTime Date { get; set; }
        public EntryStatus Status { get; set; } = EntryStatus.Unbalanced;
    }

    public enum EntryStatus
    {
        Unbalanced = 1,
        Balanced = 2
    }
}
