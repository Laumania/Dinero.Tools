using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinero.Tools.Difference.Core.Exceptions
{
    public class DataParserNotFoundException : Exception
    {
        public DataParserNotFoundException(string message) : base(message)
        {
        }
    }
}
