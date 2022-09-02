using QuirkyBookRental.Models;
using QuirkyBookRental.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuirkyBookRental.Controllers
{
    [Authorize(Roles = SD.AdminUserRole)]
    public class GenreController : Controller
    {

        private ApplicationDbContext db;

        public GenreController()
        {
            db = new ApplicationDbContext();
        }
        //Get action
        public ActionResult Create()
        {
            return View();
        }

        //Post action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            //In this point we validate the data 
            //Ex: The required information in the Genre class...
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Genre
        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Genre genre = db.Genres.Find(id);
            
            if(genre == null)
            {
                return HttpNotFound();
            }
            //Send the genre to the veiw
            return View(genre);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Genre genre = db.Genres.Find(id);

            if (genre == null)
            {
                return HttpNotFound();
            }
            //Send the genre to the veiw
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genre genre)
        {
            if (ModelState.IsValid)
            {
                //var GenreInDb = db.Genres.FirstOrDefault(g => g.id.Equals(genre.id));
                //GenreInDb.Name = genre.Name;

                db.Entry(genre).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Genre genre = db.Genres.Find(id);

            if (genre == null)
            {
                return HttpNotFound();
            }
            //Send the genre to the veiw
            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Genre genre = db.Genres.Find(id);
            db.Genres.Remove(genre);
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

    }
}