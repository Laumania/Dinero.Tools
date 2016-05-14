using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Dinero.Tools.Difference.Web.ViewModels;

namespace Dinero.Tools.Difference.Web.Controllers
{
    public class ErrorPageController : Controller
    {
        public ActionResult Error(int statusCode, Exception exception)
        {
            Response.StatusCode = statusCode;
            ViewBag.StatusCode  = statusCode + " Error";

            var viewModel = new ErrorViewModel();
            viewModel.Exception = exception;
            return View(viewModel);
        }
    }
}