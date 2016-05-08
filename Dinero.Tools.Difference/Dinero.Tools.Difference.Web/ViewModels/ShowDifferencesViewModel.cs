using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinero.Tools.Difference.Core.Models;

namespace Dinero.Tools.Difference.Web.ViewModels
{
    public class ShowDifferencesViewModel
    {
        public IEnumerable<DifferenceEntryModel> Differences { get; set; }
        public decimal TotalBank { get; set; }
        public decimal TotalDinero { get; set; }
        public decimal TotalDifference { get; set; }
    }
}
