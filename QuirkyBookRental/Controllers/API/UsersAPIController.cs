using QuirkyBookRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuirkyBookRental.Controllers.API
{
    public class UsersAPIController : ApiController
    {
        private ApplicationDbContext db;

        public UsersAPIController()
        {
            db = new ApplicationDbContext();
        }

        //To Retrieve Email or Name and BirthDate
        public IHttpActionResult Get(string type, string query = null)
        {
            if(type.Equals("email") && query != null)
            {
                var costumerQuery = db.Users.Where(u => u.Email.ToLower().Contains(query.ToLower()));

                return Ok(costumerQuery.ToList());
            }

            if (type.Equals("name") && query != null)
            {
                var costumerQuery = from u in db.Users
                                    where u.Email.Contains(query)
                                    select new
                                    {
                                        u.FirstName,
                                        u.LastName,
                                        u.BirthDate
                                    };

                return Ok(costumerQuery.ToList()[0].FirstName + " " + costumerQuery.ToList()[0].LastName
                            + ";" + costumerQuery.ToList()[0].BirthDate);
            }

            return BadRequest();
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
