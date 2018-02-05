using System;
using System.Linq;
using Dinero.Tools.Difference.Core.DataParsers;
using Dinero.Tools.Difference.Core.Exceptions;
using Dinero.Tools.Difference.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dinero.Tools.Difference.Tests
{
    [TestClass]
    public class DataParserServiceTests
    {
        [TestMethod]
        public void Parse_DanskeBankData_ReturnsDanskeBankData()
        {
            var dataToParse = @"""Dato"";""Tekst"";""Beløb"";""Saldo"";""Status"";""Afstemt""

""02.01.2014"";""DKSSL Test"";""1.455,00"";""58.185,48"";""Udført"";""Nej""

""02.01.2014"";""Nets - f. Test"";""-125,00"";""58.060,48"";""Udført"";""Ja""

""02.01.2014"";""DKSSL 0101 Test"";""1.455,00"";""59.515,48"";""Udført"";""Nej""

""02.01.2014"";""DKSSL 0101 Test 12"";""1.455,00"";""60.970,48"";""Udført"";""Nej""";

            var dataParserService = new DataParserService();
            var parseResult = dataParserService.Parse(dataToParse);

            Assert.IsNotNull(parseResult);
            Assert.IsNotNull(parseResult.DataParser);
            Assert.IsInstanceOfType(parseResult.DataParser, typeof(DanskeBankDataParser));
        }

        [TestMethod]
        public void Parse_JyskeBankData_ReturnsJyskeBankData()
        {
            var dataToParse = @"""Dato"";""Valør"";""Tekst"";"""";""Beløb"";""Saldo"";""Afstemt""
""04.02.2014"";""04.02.2014"";""02.02.14 WWW.NOGET-TEST.COM"";"""";""-79,00"";""4.353,11"";""nej"";""Til rådighed""
""04.02.2014"";""04.02.2014"";""02.02.14 TEST TEST"";"""";""-99,00"";""4.254,11"";""nej"";""Til rådighed""
""04.02.2014"";""04.02.2014"";""DK 06138 TAXA"";"""";""-326,00"";""3.928,11"";""nej"";""Til rådighed""
""05.02.2014"";""05.02.2014"";""EUROPARK A/S"";"""";""-590,00"";""3.338,11"";""nej"";""Til rådighed""
""05.02.2014"";""05.02.2014"";""DK 02693 TEST AF DANMARK"";"""";""-59,00"";""3.279,11"";""nej"";""Til rådighed""";

            var dataParserService = new DataParserService();
            var parseResult = dataParserService.Parse(dataToParse);

            Assert.IsNotNull(parseResult);
            Assert.IsNotNull(parseResult.DataParser);
            Assert.IsInstanceOfType(parseResult.DataParser, typeof(JyskeBankDataParser));
        }

        [TestMethod]
        public void Parse_NykreditBankData_ReturnsNykreditBankData()
        {
            var dataToParse = @"Konto 1234-5555558 My Company 1.455,55 DKK
Dato;Tekst;Beløb;Afstem;Saldo;Rentedato;End-to-end;Kreditorreference;Type;Adviseringer;;;
11-05-2016;Beskrivelse....;-2.981,36;N;1.455,55;11-05-2016;;;;;;;
10-05-2016;Beskrivelse....;4.300,00;N;4.436,91;10-05-2016;;;OVF;;;;
04-05-2016;Beskrivelse....;-17,85;N;136,91;04-05-2016;;;OVF;;;;
28-04-2016;Beskrivelse....;-799,00;N;154,76;28-04-2016;;;;;;;
28-04-2016;Beskrivelse....;-149,11;N;953,76;28-04-2016;;;;;;;
28-04-2016;Beskrivelse....;-4.100,64;N;1.102,87;28-04-2016;;;;;;;
28-04-2016;Beskrivelse....;-1.230,00;N;5.203,51;28-04-2016;;;;;;;
28-04-2016;RBeskrivelse....;6.150,00;N;6.433,51;28-04-2016;;;OVF;;;;
27-04-2016;Beskrivelse....;100,00;N;283,51;27-04-2016;;;OVF;;;;
27-04-2016;ZBeskrivelse....;-1.118,30;N;183,51;27-04-2016;;;;;;;
Data hentet 11-05-2016, kl. 22.46.10
";

            var dataParserService = new DataParserService();
            var parseResult = dataParserService.Parse(dataToParse);

            Assert.IsNotNull(parseResult);
            Assert.IsNotNull(parseResult.DataParser);
            Assert.IsInstanceOfType(parseResult.DataParser, typeof(NykreditBankDataParser));
        }

        [TestMethod]
        [ExpectedException(typeof(DataParserNotFoundException))]
        public void Parse_InvalidData_ReturnsNull()
        {
            var dataToParse = @"This is invalid CSV data.";
            
            var dataParserService   = new DataParserService();
            var parserResult        = dataParserService.Parse(dataToParse);
        }

        [TestMethod]
        [ExpectedException(typeof(DataParserNotFoundException))]
        public void Parse_DineroData_ThrowsException()
        {
            var dataToParse = @"Konto;Kontonavn;Dato;Bilag;Bilagstype;Tekst;Momstype;Beløb;Saldo
                                55000;Bank;2016-01-01;9;Køb;#Betalingsløsning - Quickpay;;-61,25;498309,26
                                55000;Bank;2016-01-01;20;Køb;#Forsikring;;-5065,11;493244,15
                                55000;Bank;2016-01-01;26;Køb;#Gebyr;;-800,00;492444,15
                                55000;Bank;2016-01-06;3;Køb;#Software - Github;;-593,84;491850,31";

            var dataParserService   = new DataParserService();
            var parserResult        = dataParserService.Parse(dataToParse);
        }
    }
}
