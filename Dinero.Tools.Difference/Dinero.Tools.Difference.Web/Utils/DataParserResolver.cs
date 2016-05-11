using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dinero.Tools.Difference.Core.Interfaces;
using Dinero.Tools.Difference.DataParsers;

namespace Dinero.Tools.Difference.Web.Utils
{
    public static class DataParserResolver
    {
        public static IDataParser ResolveParser(string data)
        {
            var parsers = new IDataParser[]
            {
                new ArbejdernesLandsBankDataParser(), 
                new DanskeBankDataParser(), 
                new JyskeBankDataParser(), 
                new NordeaDataParser(), 
                new SaxoBankDataParser(), 
            };

            foreach (var dataParser in parsers)
            {
                try
                {
                    var parsedResult = dataParser.Parse(data);

                    if (parsedResult != null && parsedResult.Any())
                        return dataParser;
                }
                catch (Exception ex)
                {
                    //On purpose we hide exceptions here - eventhough this type of catch normally is a totally no-go!
                    //Don't try this at home/work kids!
                }
            }

            return null;
        }
    }
}