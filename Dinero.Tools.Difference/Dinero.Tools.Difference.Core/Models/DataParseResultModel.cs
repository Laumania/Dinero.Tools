using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinero.Tools.Difference.Core.Interfaces;

namespace Dinero.Tools.Difference.Core.Models
{
    public class DataParseResultModel
    {
        public IDataParser DataParser { get; set; }
        public IEnumerable<EntryModel> BankEntries { get; set; }
    }
}
