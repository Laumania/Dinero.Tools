using System;
using Dinero.Tools.Difference.DataParsers;
using Dinero.Tools.Difference.Web.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dinero.Tools.Difference.Tests
{
    [TestClass]
    public class DataParserResolverTests
    {
        [TestMethod]
        public void ResolveParser_DanskeBankData_ReturnsDanskeBankDataParser()
        {
            var dataToParse = @"""Dato"";""Tekst"";""Beløb"";""Saldo"";""Status"";""Afstemt""

""02.01.2014"";""DKSSL Test"";""1.455,00"";""58.185,48"";""Udført"";""Nej""

""02.01.2014"";""Nets - f. Test"";""-125,00"";""58.060,48"";""Udført"";""Ja""

""02.01.2014"";""DKSSL 0101 Test"";""1.455,00"";""59.515,48"";""Udført"";""Nej""

""02.01.2014"";""DKSSL 0101 Test 12"";""1.455,00"";""60.970,48"";""Udført"";""Nej""";

            var foundParser = DataParserResolver.ResolveParser(dataToParse);

            Assert.IsNotNull(foundParser);
            Assert.IsInstanceOfType(foundParser, typeof(DanskeBankDataParser));
        }

        [TestMethod]
        public void ResolveParser_InvalidData_ReturnsNull()
        {
            var dataToParse = @"This is invalid CSV data.";

            var foundParser = DataParserResolver.ResolveParser(dataToParse);

            Assert.IsNull(foundParser);
        }
    }
}
