﻿using QuirkyBookRental.Models;
using QuirkyBookRental.Utility;
using QuirkyBookRental.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QuirkyBookRental.Controllers
{
    [Authorize(Roles = SD.AdminUserRole)]
    public class UserController : Controller
    {

        private ApplicationDbContext db;

        public UserController()
        {
            db = ApplicationDbContext.Create();
        }

        // GET: User
        public ActionResult Index()
        {

            var user = from u in db.Users
                       join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                       select new UserViewModel
                       {
                           Id = u.Id,
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Email = u.Email,
                           Phone = u.Phone,
                           BirthDate = u.BirthDate,
                           MemberShipTypeId = u.MembershipTypeId,
                           MembershipTypes = (ICollection<MembershipType>)db.MembershipTypes.ToList().Where(n => n.Id.Equals(u.MembershipTypeId)),
                           Disable = u.Disable

                       };

            var userList = user.ToList();

            return View(userList);
        }

        //EDIT Get
        public ActionResult Edit(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            UserViewModel model = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Id = user.Id,
                MemberShipTypeId = user.MembershipTypeId,
                MembershipTypes = db.MembershipTypes.ToList(),
                Phone = user.Phone,
                Disable = user.Disable
            };

            return View(model);
        }

        //POST method por EDIT action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                UserViewModel model = new UserViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BirthDate = user.BirthDate,
                    Email = user.Email,
                    Id = user.Id,
                    MemberShipTypeId = user.MemberShipTypeId,
                    MembershipTypes = db.MembershipTypes.ToList(),
                    Phone = user.Phone,
                    Disable = user.Disable
                };
                return View("Edit", model);
            }
            else
            {
                var userInDb = db.Users.Single(u => u.Id == user.Id);

                userInDb.FirstName = user.FirstName;
                userInDb.LastName = user.LastName;
                userInDb.Email = user.Email;
                userInDb.BirthDate = user.BirthDate;
                userInDb.MembershipTypeId = user.MemberShipTypeId;
                userInDb.Phone = user.Phone;
                userInDb.Disable = user.Disable;

            }

            db.SaveChanges();

            return RedirectToAction("Index", "User");


        }


        public ActionResult Details(string id)
        {

            if(id == null || id.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = db.Users.Find(id);

            UserViewModel model = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Id = user.Id,
                MemberShipTypeId = user.MembershipTypeId,
                MembershipTypes = db.MembershipTypes.ToList(),
                Phone = user.Phone,
                Disable = user.Disable
            };



            return View(model);
        }

        //Delete get
        public ActionResult Delete(string id)
        {

            if (id == null || id.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = db.Users.Find(id);

            UserViewModel model = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                Id = user.Id,
                MemberShipTypeId = user.MembershipTypeId,
                MembershipTypes = db.MembershipTypes.ToList(),
                Phone = user.Phone,
                Disable = user.Disable
            };



            return View(model);
        }

        //Delete post method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var userInDb = db.Users.Find(id);

            if (userInDb == null || id.Length == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            userInDb.Disable = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

    }
}