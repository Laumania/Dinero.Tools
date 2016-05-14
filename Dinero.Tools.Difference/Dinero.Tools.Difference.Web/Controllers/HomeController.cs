using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dinero.Tools.Difference.Core.DataParsers;
using Dinero.Tools.Difference.Core.Interfaces;
using Dinero.Tools.Difference.Core.Models;
using Dinero.Tools.Difference.Core.Services;
using Dinero.Tools.Difference.Web.ViewModels;

namespace Dinero.Tools.Difference.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowDifferences(HttpPostedFileBase dineroFile, HttpPostedFileBase bankFile)
        {
            var viewModel = new ShowDifferencesViewModel();

            if (ModelState.IsValid)
            {
                var dineroCsvContent        = GetFileContent(dineroFile);
                var bankCsvContent          = GetFileContent(bankFile);

                if (dineroCsvContent == null)
                    throw new Exception("Hov, tror du manglede at vælge din Dinero fil.");
                if (bankCsvContent == null)
                    throw new Exception("Hov, tror sørme du manglede at vælge din bank fil.");

                var dineroDataParser        = new DineroDataParser();
                var dineroEntries           = dineroDataParser.Parse(dineroCsvContent);

                var dataParserService       = new DataParserService();
                var parserResult            = dataParserService.Parse(bankCsvContent);


                var difService              = new DifferenceService();

                viewModel.Differences       = difService.FindDifferences(dineroEntries, parserResult.BankEntries);
                viewModel.LatestBankEntry   = GetLastestEntryEqualOrLowerThanToday(parserResult.BankEntries);
                viewModel.LatestDineroEntry = GetLastestEntryEqualOrLowerThanToday(dineroEntries);
                viewModel.TotalDifference   = viewModel.LatestDineroEntry.Saldo - viewModel.LatestBankEntry.Saldo;
            }
            
            return View(viewModel);
        }

        private EntryModel GetLastestEntryEqualOrLowerThanToday(IEnumerable<EntryModel> entries)
        {
            var entry = entries.Where(x => x.Date <= DateTime.Today).OrderByDescending(x => x.Index).First();
            return entry;
        }

        private string GetFileContent(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var stringContent = "";
                using (var reader = new System.IO.BinaryReader(file.InputStream))
                {
                    var content = reader.ReadBytes(file.ContentLength);
                    stringContent = System.Text.Encoding.UTF8.GetString(content);
                }
                return stringContent;
            }
            else
                return null;
        }
    }
}