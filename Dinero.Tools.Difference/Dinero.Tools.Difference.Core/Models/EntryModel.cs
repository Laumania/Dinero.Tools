using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineroKontoComparer.Core.Models
{
    public class EntryModel
    {
        public string Text { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
