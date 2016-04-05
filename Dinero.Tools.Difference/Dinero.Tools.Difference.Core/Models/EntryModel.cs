using System;
using System.Diagnostics;

namespace Dinero.Tools.Difference.Core.Models
{
    [DebuggerDisplay("Amount: {Amount}, Date: {Date}, Text: {Text}")]
    public class EntryModel
    {
        public string Text { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
