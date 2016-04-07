using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dinero.Tools.Difference.Web.Filters
{
    public class ForceDanishCultureFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            Thread.CurrentThread.CurrentCulture     = CultureInfo.GetCultureInfo("da-DK");
            Thread.CurrentThread.CurrentUICulture   = CultureInfo.GetCultureInfo("da-DK");
        }
    }
}
