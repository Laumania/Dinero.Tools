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
        public void FindDifferences_OneMoreEntryInBank_ReturnOneFoundOnlyOnLocalAccount()
        {
            var differenceService   = new DifferenceService();

            var dineroEntries       = new List<EntryModel>()
            {
                new EntryModel() { Amount = 100.00m, Date = DateTime.Now.AddDays(-1)}
            };

            var bankEntries         = new List<EntryModel>()
            {
                new EntryModel() { Amount = 100.00m, Date = DateTime.Now.AddDays(-1)},
                new EntryModel() { Amount = 25.00m, Date = DateTime.Now.AddDays(-4)}
            };

            var result = differenceService.FindDifferences(dineroEntries, bankEntries);

            Assert.AreEqual(1, result.OriginalBankEntries.Count(x => x.Status == EntryStatus.Unbalanced));
            Assert.AreEqual(-25.00m, result.TotalDifference);
        }

        [TestMethod]
        public void FindDifferences_OneMoreEntryInDinero_ReturnOneUnbalancedInDinero()
        {
            var differenceService = new DifferenceService();

            var dineroEntries = new List<EntryModel>()
            {
                new EntryModel() { Amount = 100.00m, Date = DateTime.Now.AddDays(-1)},
                new EntryModel() { Amount = 25.00m, Date = DateTime.Now.AddDays(-4)}
            };

            var bankEntries = new List<EntryModel>()
            {
                new EntryModel() { Amount = 100.00m, Date = DateTime.Now.AddDays(-1)}
            };

            var result = differenceService.FindDifferences(dineroEntries, bankEntries);

            Assert.AreEqual(1, result.OriginalDineroEntries.Count(x => x.Status == EntryStatus.Unbalanced));
            Assert.AreEqual(25.00m, result.TotalDifference);
        }

        [TestMethod]
        public void FindDifferences_TwoMoreEntriesInBank_ReturnTwoUnbalacedInBank()
        {
            var differenceService = new DifferenceService();

            var dineroEntries = new List<EntryModel>()
            {
                new EntryModel() { Amount = 100.00m, Date = DateTime.Now.AddDays(-1)},
                new EntryModel() { Amount = 25.00m, Date = DateTime.Now.AddDays(-4)}
            };

            var bankEntries = new List<EntryModel>()
            {
                new EntryModel() { Amount = 100.00m, Date = DateTime.Now.AddDays(-1)},
                new EntryModel() { Amount = 35.00m, Date = DateTime.Now.AddDays(-1)},
                new EntryModel() { Amount = 45.00m, Date = DateTime.Now.AddDays(-1)},
            };

            var result = differenceService.FindDifferences(dineroEntries, bankEntries);

            Assert.AreEqual(2, result.OriginalBankEntries.Count(x => x.Status == EntryStatus.Unbalanced));
            Assert.AreEqual(1, result.OriginalDineroEntries.Count(x => x.Status == EntryStatus.Unbalanced));
            Assert.AreEqual(-55.00m, result.TotalDifference);
        }

        [TestMethod]
        public void FindDifferences_OneMoreEntryInBankWithSameAmount_ReturnOneUnbalacedInBank()
        {
            var differenceService = new DifferenceService();

            var dineroEntries = new List<EntryModel>()
            {
                new EntryModel() { Amount = 100.00m, Date = DateTime.Now.AddDays(-1)},
                new EntryModel() { Amount = 25.00m, Date = DateTime.Now.AddDays(-4)}
            };

            var bankEntries = new List<EntryModel>()
            {
                new EntryModel() { Amount = 100.00m, Date = DateTime.Now.AddDays(-1)},
                new EntryModel() { Amount = 25.00m, Date = DateTime.Now.AddDays(-1)},
                new EntryModel() { Amount = 25.00m, Date = DateTime.Now.AddDays(-1)},
            };

            var result = differenceService.FindDifferences(dineroEntries, bankEntries);

            Assert.AreEqual(1, result.OriginalBankEntries.Count(x => x.Status == EntryStatus.Unbalanced));
            Assert.AreEqual(0, result.OriginalDineroEntries.Count(x => x.Status == EntryStatus.Unbalanced));
            Assert.AreEqual(-25.00m, result.TotalDifference);
        }
    }
}
