using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anew.AnewModels;
using Anew.AnewAPIAdapter;

namespace AnewWeb.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            FlightSearchRq flightSearchRq = new FlightSearchRq();
            return View(flightSearchRq);
        }

        [HttpPost]
        public ActionResult Index(FlightSearchRq model)
        {
            FlightSearchRs flightSearchRs = new FlightSearchRs();
            AnewSearch search = new AnewSearch();

            model.CityPares[1].Origin = model.CityPares[0].Destination;
            model.CityPares[1].Destination = model.CityPares[0].Origin;

            if (ModelState.IsValid)
            {
                flightSearchRs = search.Search(model);
                return View("DoSearch", flightSearchRs);
            }
            else
            {
                return View();
            }
        }
    }
}
