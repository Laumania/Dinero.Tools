using System;
using System.Linq;
using Dinero.Tools.Difference.DataParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dinero.Tools.Difference.Tests
{
    [TestClass]
    public class NordeaDataParserTests
    {
        [TestMethod]
        public void Parse_ValidInputWith4Entries_Return4Entries()
        {
            var parser = new NordeaDataParser();
            var dummyData = @"Bogført;Tekst;Rentedato;Beløb;Saldo
                                01-04-2016;Dankort-nota quickpay.net   351-1;01-04-2016;-62,13;335975,98
                                01-04-2016;Overførsel;01-04-2016;-37,50;336038,11
                                31-03-2016;Bs betaling TDC A/S;31-03-2016;-344,00;336075,61
                                31-03-2016;Bgs Udbytte;30-03-2016;-164000,00;336419,61";

            var result = parser.Parse(dummyData);

            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(-62.13m, result.First().Amount);
        }
    }
}
