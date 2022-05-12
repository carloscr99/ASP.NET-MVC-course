using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuirkyBookRental
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute("BookkByReleaseMonth",
            //                "Book/ReleaseMonth/{year}/{month}",
            //                new { Controller = "book", Action = "ReleaseMonth"});

            //routes.MapRoute("BookkByReleaseYearAndAuthor",
            //                "Book/ReleaseYearAndAuthor/{year}/{author}",
            //                new { Controller = "book", Action = "ReleaseYearAndAuthor" },
            //                constraints: new { year = @"\d{4}" }
            //                ); //We made and regular expresion to the year. It will be 4 int characters 

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
