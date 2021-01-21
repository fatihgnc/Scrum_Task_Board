using Scrum_Task_Board.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Scrum_Task_Board.Controllers
{
    public class CardController : Controller
    {
        private DatabaseContext db;
        // GET: Card
        public ActionResult Add()
        {
            Card card = null;

            return View(card);
        }


        /// <summary>
        ///  Kart eklemede kullanılan fonksiyon
        /// </summary>
        /// <param name="c"></param>
        [HttpPost]
        public ActionResult Add(Card c)
        {
            db = new DatabaseContext();
            db.Card.Add(c);

            int sonuc = db.SaveChanges();

            ViewBag.Message = sonuc > 0 ? "Kart başarıyla eklendi!" : "Kart eklenirken bir sorun oluştu!";
            ViewBag.Alert = sonuc > 0 ? "alert-success" : "alert-danger";

            return View();
        }

        public ActionResult Edit(int? cardID)
        {
            Card card = null;

            if(cardID != null)
            {
                db = new DatabaseContext();
                card = db.Card.Where(c => c.cardID == cardID).FirstOrDefault();
            }
            else
                return RedirectToAction("NoParamsProvidedError", "Error");
            return View(card);
        }

        /// <summary>
        ///  Kart düzenlemede kullanılan fonksiyon
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cardID"></param>
        [HttpPost]
        public ActionResult Edit(Card model, int? cardID)
        {
            db = new DatabaseContext();
            Card card = db.Card.Where(x => x.cardID == cardID).FirstOrDefault(); ;

            if (card != null)
            {
                card.cardExpert = model.cardExpert;
                card.cardName = model.cardName;
                card.cardNotes = model.cardNotes;
                card.cardDefinition = model.cardDefinition;
                card.cardActualTime = model.cardActualTime;
                card.cardEstimatedTime = model.cardEstimatedTime;
                card.cardNo = model.cardNo;
                card.cardCreationDate = model.cardCreationDate;

                int sonuc = db.SaveChanges();

                ViewBag.Message = sonuc > 0 ? "Kart başarıyla güncellendi!" : "Kart güncellenirken bir sorun oluştu!";
                ViewBag.Alert = sonuc > 0 ? "alert-success" : "alert-danger";
            }

            return View();
        }

        public ActionResult Delete(int? cardID)
        {
            Card card = null;

            if(cardID != null)
            {
                db = new DatabaseContext();
                card = db.Card.Where(c => c.cardID == cardID).FirstOrDefault();
            }
            else
                return RedirectToAction("NoParamsProvidedError", "Error");

            return View(card);
        }


        /// <summary>
        ///  Kart silmede kullanılan fonksiyon
        /// </summary>
        /// <param name="cardID"></param>
        [HttpPost, ActionName("Delete")]
        public ActionResult Deletee(int? cardID)
        {
            if (cardID != null)
            {
                db = new DatabaseContext();
                Card card = db.Card.Where(c => c.cardID == cardID).FirstOrDefault();

                db.Card.Remove(card);

                int sonuc = db.SaveChanges();

                ViewBag.Message = sonuc > 0 ? "Kart silindi." : "Kart silinirken bir hata oluştu!";
                ViewBag.Alert = sonuc > 0 ? "alert-success" : "alert-danger";
            }

            return RedirectToAction("Cards","App");
        }

        public ActionResult DeleteAll()
        {
            db = new DatabaseContext();
            foreach (var card in db.Card)
                db.Card.Remove(card);
            
            db.SaveChanges();

            return RedirectToAction("Cards", "App", null);
        }

        public ActionResult CardDetail(int? cardID)
        {
            Card card = null;

            if (cardID != null)
            {
                db = new DatabaseContext();
                card = db.Card.Where(c => c.cardID == cardID).FirstOrDefault();
                card.Task = db.Task.Where(t => t.cardID == cardID).ToList();
                if (Session["cardID"] == null)
                    Session["cardID"] = cardID;
            }
            else
                return RedirectToAction("NoParamsProvidedError", "Error");

            return View(card);
        }
    }
}