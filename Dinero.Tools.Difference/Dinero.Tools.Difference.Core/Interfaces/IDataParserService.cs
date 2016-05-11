using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinero.Tools.Difference.Core.Models;

namespace Dinero.Tools.Difference.Core.Interfaces
{
    public interface IDataParserService
    {
        DataParseResultModel Parse(string data);
    }
}
