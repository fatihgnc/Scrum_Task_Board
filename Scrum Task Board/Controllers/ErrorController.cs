using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Scrum_Task_Board.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NoParamsProvidedError()
        {
            return View();
        }
    }
}