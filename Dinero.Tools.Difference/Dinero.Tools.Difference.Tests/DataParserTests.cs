using System;
using System.Linq;
using Dinero.Tools.Difference.DataParsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dinero.Tools.Difference.Tests
{
    [TestClass]
    public class DataParserTests
    {
        [TestMethod]
        public void DineroParse_ValidInputWith4Entries_Return4Entries()
        {
            var parser = new DineroDataParser();
            var dummyData = @"Konto;Kontonavn;Dato;Bilag;Bilagstype;Tekst;Momstype;Beløb;Saldo
                                55000;Bank;2016-01-01;9;Køb;#Betalingsløsning - Quickpay;;-61,25;498309,26
                                55000;Bank;2016-01-01;20;Køb;#Forsikring;;-5065,11;493244,15
                                55000;Bank;2016-01-01;26;Køb;#Gebyr;;-800,00;492444,15
                                55000;Bank;2016-01-06;3;Køb;#Software - Github;;-593,84;491850,31";

            var result = parser.Parse(dummyData);

            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(-61.25m, result.First().Amount);
        }

        [TestMethod]
        public void NordeaParse_ValidInputWith4Entries_Return4Entries()
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
