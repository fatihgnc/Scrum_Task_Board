using Scrum_Task_Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Scrum_Task_Board.Controllers
{
    public class AppController : Controller
    {
        private DatabaseContext db;
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Cards()
        {
            db = new DatabaseContext();
            var cards = db.Card.ToList();
            return View(cards);
        }

    }
}