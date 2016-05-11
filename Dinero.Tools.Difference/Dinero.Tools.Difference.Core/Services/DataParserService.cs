using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dinero.Tools.Difference.Core.DataParsers;
using Dinero.Tools.Difference.Core.Interfaces;
using Dinero.Tools.Difference.Core.Models;

namespace Dinero.Tools.Difference.Core.Services
{
    public class DataParserService : IDataParserService
    {
        public DataParseResultModel Parse(string data)
        {
            var parsers = new IDataParser[]
            {
                new ArbejdernesLandsBankDataParser(),
                new DanskeBankDataParser(),
                new JyskeBankDataParser(),
                new NordeaDataParser(),
                new SaxoBankDataParser(),
                new NykreditBankDataParser(), 
            };

            foreach (var dataParser in parsers)
            {
                try
                {
                    var parsedResult = dataParser.Parse(data);
                    if (parsedResult != null && parsedResult.Any())
                    {
                        var result = new DataParseResultModel()
                        {
                            DataParser  = dataParser,
                            BankEntries = parsedResult
                        };
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    //Intentionally we hide exceptions here - even though this type of catch normally is a totally no-go!
                    //Don't try this at home/work kids!
                }
            }

            return null;
        }
    }
}
