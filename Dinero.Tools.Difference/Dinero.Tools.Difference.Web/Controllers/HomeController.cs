using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dinero.Tools.Difference.Core.Interfaces;
using Dinero.Tools.Difference.Core.Models;
using Dinero.Tools.Difference.Core.Services;
using Dinero.Tools.Difference.DataParsers;
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
                var dineroCsvContent    = GetFileContent(dineroFile);
                var bankCsvContent      = GetFileContent(bankFile);

                if (dineroCsvContent == null || bankCsvContent == null)
                    throw new ArgumentNullException("Something is wrong with one of the files...");

                var dineroDataParser    = new DineroDataParser();
                var dineroEntries       = dineroDataParser.Parse(dineroCsvContent);

                var bankDataParser      = new NordeaDataParser();
                var bankEntries         = bankDataParser.Parse(bankCsvContent);


                var difService          = new DifferenceService();

                viewModel.Differences       = difService.FindDifferences(dineroEntries, bankEntries);
                viewModel.TotalBank         = GetLastestEntryEqualOrLowerThanToday(bankEntries).Saldo;
                viewModel.TotalDinero       = GetLastestEntryEqualOrLowerThanToday(dineroEntries).Saldo;
                viewModel.TotalDifference   = viewModel.TotalDinero - viewModel.TotalBank;
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