﻿using QuirkyBookRental.Models;
using QuirkyBookRental.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuirkyBookRental.Controllers.API
{
    public class BookAPIController : ApiController
    {

        private ApplicationDbContext db;

        public BookAPIController()
        {
            db = new ApplicationDbContext();
        }


        public IHttpActionResult Get(string query = null)
        {
            var bookQuery = db.Books.Where(b => b.Title.ToLower().Contains(query.ToLower()));

            return Ok(bookQuery.ToList());
        }

        //Price or Availability (price/avail)
        public IHttpActionResult Get(string type, string isbm = null, string rentalDuration= null, string email = null)
        {

            if (type.Equals("price"))
            {
                Book BookQuery = db.Books.Where(b => b.ISBN.Equals(isbm)).SingleOrDefault();

                var chargeRate = from u in db.Users
                                 join m in db.MembershipTypes on u.MembershipTypeId equals m.Id
                                 where u.Email.Equals(email)
                                 select new {m.ChargeRateOneMonth, m.ChargeRateSixMonth};

                var price = Convert.ToDouble(BookQuery.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateOneMonth) / 100;

                if(rentalDuration == SD.SixMonthCount)
                {
                    price = Convert.ToDouble(BookQuery.Price) * Convert.ToDouble(chargeRate.ToList()[0].ChargeRateSixMonth) / 100;
                }
                return Ok(price);
            }
            else
            {
                Book BookQuery = db.Books.Where(b => b.ISBN.Equals(isbm)).SingleOrDefault();


                return Ok(BookQuery.Avaibility);
            }

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
