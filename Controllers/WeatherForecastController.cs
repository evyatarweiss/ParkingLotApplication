using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PLotAPI.ParkingLotObjects;
using PLotAPI.ParkingLotServices;

namespace PLotAPI.Controllers
{
    public class ParkingLotAssignmentContorller : ControllerBase
    {
        ParkingLotService ParkingLotService;
        public ParkingLotAssignmentContorller()
        {
            ParkingLotService = new ParkingLotService();
        }

        [HttpPost]
        [Route("/entry")]
        public ContentResult EntryToParkingLot([FromBody] EntryObject LicensePlateAndParkingLotID)
        {
            ParkingLotService.NullCheck(LicensePlateAndParkingLotID, nameof(LicensePlateAndParkingLotID));
            var ticketId = ParkingLotService.GetTicketId(LicensePlateAndParkingLotID);
            Response.StatusCode = 200;

            return Content(JsonConvert.SerializeObject(ticketId));
        }

        [HttpPost]
        [Route("/exit")]
        public ContentResult ExitFromParkingLot([FromBody] int ticketId)
        {
            var ExitObj = ParkingLotService.GetOut(ticketId);
            return Content(JsonConvert.SerializeObject(ExitObj));
        }
    }
}