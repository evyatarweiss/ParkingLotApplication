using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using ParkingLotAssignment.ParkingLotObjects;
using ParkingLotAssignment.ParkingLotServices;

namespace ParkingLotAssignment.Controllers
{
    public class ParkingLotAssignmentContorller : Controller
    {
        ParkingLotService ParkingLotService;
        public ParkingLotAssignmentContorller()
        {
            ParkingLotService = new ParkingLotService();
        }
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("/entry")]
        public ContentResult EntryToParkingLot([FromBody] EntryObject LicensePlateAndParkingLotID)
        {
            ParkingLotService.NullCheck(LicensePlateAndParkingLotID, nameof(LicensePlateAndParkingLotID));
            var ticketId = ParkingLotService.GetTicketId(LicensePlateAndParkingLotID);
            Response.StatusCode = 200;

            return Content(JsonConvert.SerializeObject(ticketId));
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("/exit")]
        public ContentResult ExitFromParkingLot([FromBody] int ticketId)
        {
            var ExitObj = ParkingLotService.GetOut(ticketId);
            return Content(JsonConvert.SerializeObject(ExitObj));
        }
    }
}