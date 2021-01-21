using Scrum_Task_Board.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Scrum_Task_Board.Controllers
{
    public class TaskController : Controller
    {
        private DatabaseContext db;
        // GET: Task
        
        public ActionResult Add(int? cardID)
        {
            db = new DatabaseContext();
            Task task = null;
            List<SelectListItem> states = new List<SelectListItem>();
            var state = db.State.ToList();

            foreach(var s in state)
            {
                states.Add(new SelectListItem() { Text = s.stateName, Value = s.stateID.ToString() });
            }

            ViewBag.states = states;
            

            if (cardID != null)
            {
                Session["cardID"] = cardID;
                return View(task);
            }
            else
                return RedirectToAction("NoParamsProvidedError","Error");
        }

        /// <summary>
        ///  İş eklemede kullanılan fonksiyon
        /// </summary>
        /// <param name="t"></param>
        [HttpPost]
        public ActionResult Add(Task t)
        {
            db = new DatabaseContext();
            
            if(t != null)
            {
                db.Task.Add(t);
                int sonuc = db.SaveChanges();

                ViewBag.Message = sonuc > 0 ? "İş başarıyla eklendi!" : "İş eklenirken bir sorun oluştu!";
                ViewBag.Alert = sonuc > 0 ? "alert-success" : "alert-danger";
                List<SelectListItem> states = new List<SelectListItem>();
                var state = db.State.ToList();

                foreach (var s in state)
                {
                    states.Add(new SelectListItem() { Text = s.stateName, Value = s.stateID.ToString() });
                }

                ViewBag.states = states;
                return View();
            }

            return RedirectToAction("CardDetail","Card", new { cardID = t.cardID});
        }

        public ActionResult NoParamsProvidedError()
        {
            return View();
        }

        public ActionResult Edit(int? taskID)
        {
            Task task = null;
            db = new DatabaseContext();
           
            List<SelectListItem> states = new List<SelectListItem>();
            var state = db.State.ToList();

            foreach (var s in state)
            {
                states.Add(new SelectListItem() { Text = s.stateName, Value = s.stateID.ToString() });
            }

            ViewBag.states = states;

            if (taskID != null)                
                task = db.Task.Where(t => t.taskID == taskID).FirstOrDefault();
            else
                return RedirectToAction("NoParamsProvidedError", "Error");

            return View(task);
        }

        /// <summary>
        ///  İş düzenlemede kullanılan fonksiyon
        /// </summary>
        /// <param name="t"></param>
        [HttpPost]
        public ActionResult Edit(Task t)
        {
            Task task = null;
            if(t != null)
            {
                db = new DatabaseContext();
                task = db.Task.Where(x => x.taskID == t.taskID).FirstOrDefault();
                task.taskDate = t.taskDate;
                task.taskDefinition = t.taskDefinition;
                task.taskHeader = t.taskHeader;
                task.taskStateID = t.taskStateID;

                db.SaveChanges();
            }

            return RedirectToAction("CardDetail", "Card", new { cardID = t.cardID });
        }

        public ActionResult Delete(int? taskID)
        {
            Task task = null;
            db = new DatabaseContext();

            List<SelectListItem> states = new List<SelectListItem>();
            var state = db.State.ToList();

            foreach (var s in state)
            {
                states.Add(new SelectListItem() { Text = s.stateName, Value = s.stateID.ToString() });
            }

            ViewBag.states = states;

            if (taskID != null)
                task = db.Task.Where(t => t.taskID == taskID).FirstOrDefault();
            else
                return RedirectToAction("NoParamsProvidedError", "Error");
            return View(task);
        }

        /// <summary>
        ///  İş silmede kullanılan fonksiyon
        /// </summary>
        /// <param name="taskID"></param>
        [HttpPost, ActionName("Delete")]
        public ActionResult Deletee(int? taskID)
        {
            db = new DatabaseContext();
            Task task = null;

            if(taskID != null)
            {
                task = db.Task.Where(t => t.taskID == taskID).FirstOrDefault();
                db.Task.Remove(task);
                db.SaveChanges();
            }

            return RedirectToAction("CardDetail", "Card", new { cardID = task.cardID });
        }

        public ActionResult TaskDetail(int? taskID, int? cardID)
        {
            Task task = null;

            if(cardID != null)
            {
                if (Session["cardID"] == null)
                    Session["cardID"] = cardID;
            }

            if(taskID != null)
            {
                db = new DatabaseContext();
                task = db.Task.Where(x => x.taskID == taskID).FirstOrDefault();
            }
            else
                return RedirectToAction("NoParamsProvidedError", "Error");

            return View(task);
        }

        public void updateTasks(Task model)
        {
            db = new DatabaseContext();
            Task task = db.Task.Where(x => x.taskID == model.taskID).FirstOrDefault();
            if(task != null)
            {
                task.taskStateID = model.taskStateID;
                db.SaveChanges();
            }
        }
    }
}