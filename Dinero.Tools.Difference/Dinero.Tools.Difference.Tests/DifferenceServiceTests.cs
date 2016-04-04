using System;
using System.Collections.Generic;
using System.Linq;
using Dinero.Tools.Difference.Core.Models;
using Dinero.Tools.Difference.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dinero.Tools.Difference.Tests
{
    [TestClass]
    public class DifferenceServiceTests
    {
        [TestMethod]
        public void FindDifferences_ValidListsWithOneMissingEntry_ReturnDataWithOneMissingEntry()
        {
            var differenceService   = new DifferenceService();

            var dineroEntries       = new List<EntryModel>()
            {
                new EntryModel() { Amount = 100.00m, Date = DateTime.Now.AddDays(-1), Text = "Dinero Test entry 1" },
                new EntryModel() { Amount = 50.00m, Date = DateTime.Now.AddDays(-2), Text = "Dinero Test entry 2" }
            };

            var bankEntries         = new List<EntryModel>()
            {
                new EntryModel() { Amount = 100.00m, Date = DateTime.Now.AddDays(-1), Text = "Bank Test entry 1" },
                new EntryModel() { Amount = 50.00m, Date = DateTime.Now.AddDays(-2), Text = "Bank Test entry 2" },
                new EntryModel() { Amount = 50.00m, Date = DateTime.Now.AddDays(-3), Text = "Bank Test entry 3" },
                new EntryModel() { Amount = 25.00m, Date = DateTime.Now.AddDays(-4), Text = "Bank Test entry 4" }
            };

            var result = differenceService.FindDifferences(dineroEntries, bankEntries);

            Assert.IsTrue(result.DifferenceEntries.Count() == 1);
        }
    }
}
