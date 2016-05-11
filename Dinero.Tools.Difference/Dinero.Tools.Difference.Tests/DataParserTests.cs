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
            Assert.AreEqual(-61.25m, result.First(x => x.Index == 0).Amount);
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
            Assert.AreEqual(-164000m, result.First(x => x.Index == 0).Amount);
        }

        [TestMethod]
        public void DanskeBankParse_ValidInputWith4Entries_Return4Entries()
        {
            var parser = new DanskeBankDataParser();
            //Double qoutes are just here, to make it fit inside a string in C#.
            //The below sample cannot be indented, to match the style. If have to be where it is.
            var dummyData = @"""Dato"";""Tekst"";""Beløb"";""Saldo"";""Status"";""Afstemt""

""02.01.2014"";""DKSSL Test"";""1.455,00"";""58.185,48"";""Udført"";""Nej""

""02.01.2014"";""Nets - f. Test"";""-125,00"";""58.060,48"";""Udført"";""Ja""

""02.01.2014"";""DKSSL 0101 Test"";""1.455,00"";""59.515,48"";""Udført"";""Nej""

""02.01.2014"";""DKSSL 0101 Test 12"";""1.455,00"";""60.970,48"";""Udført"";""Nej""";

            var result = parser.Parse(dummyData);

            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(1455m, result.First(x => x.Index == 0).Amount);
        }

        [TestMethod]
        public void JyskeBankParse_ValidInputWith4Entries_Return4Entries()
        {
            var parser = new JyskeBankDataParser();
            //Double qoutes are just here, to make it fit inside a string in C#.
            //The below sample cannot be indented, to match the style. If have to be where it is.
            var dummyData = @"""Dato"";""Valør"";""Tekst"";"""";""Beløb"";""Saldo"";""Afstemt""
""04.02.2014"";""04.02.2014"";""02.02.14 WWW.NOGET-TEST.COM"";"""";""-79,00"";""4.353,11"";""nej"";""Til rådighed""
""04.02.2014"";""04.02.2014"";""02.02.14 TEST TEST"";"""";""-99,00"";""4.254,11"";""nej"";""Til rådighed""
""04.02.2014"";""04.02.2014"";""DK 06138 TAXA"";"""";""-326,00"";""3.928,11"";""nej"";""Til rådighed""
""05.02.2014"";""05.02.2014"";""EUROPARK A/S"";"""";""-590,00"";""3.338,11"";""nej"";""Til rådighed""
""05.02.2014"";""05.02.2014"";""DK 02693 TEST AF DANMARK"";"""";""-59,00"";""3.279,11"";""nej"";""Til rådighed""
";
            var result = parser.Parse(dummyData);

            Assert.AreEqual(5, result.Count());
            Assert.AreEqual(-79m, result.First(x => x.Index == 0).Amount);
        }

        [TestMethod]
        public void ArbejdernesLandsBankParse_ValidInputWith4Entries_Return4Entries()
        {
            var parser = new ArbejdernesLandsBankDataParser();
            //Double qoutes are just here, to make it fit inside a string in C#.
            //The below sample cannot be indented, to match the style. If have to be where it is.
            var dummyData = @"20-02-2014;""Standardovf. 333333333"";-500,00; 54480,76;""20-02-2014"";
20-02-2014;""Standardovf. rødby"";-5040,00; 49440,76;""20-02-2014"";
20-02-2014;""GIRO/FI Vanløse"";-1636,00; 47804,76;""20-02-2014"";
21-02-2014;""Standardovf. 222222222"";-1000,00; 46804,76;""21-02-2014"";
27-02-2014;""Standardovf. 111111111""; 8000,00; 54804,76;""27-02-2014"";
28-02-2014;""GIRO/FI bil"";-1920,00; 52884,76;""28-02-2014"";
03-03-2014;""BS TEST Overførsel"";-150,00; 52734,76;""03-03-2014"";
";
            var result = parser.Parse(dummyData);

            Assert.AreEqual(7, result.Count());
            Assert.AreEqual(-500m, result.First(x => x.Index == 0).Amount);
        }
    }
}
