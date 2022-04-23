using ParkingLotAssignment.ParkingLotObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ParkingLotAssignment.ParkingLotServices
{
    public class ParkingLotService
    {
        public ParkingLotService()
        {

        }
        static ConcurrentDictionary<int, ParkingStatus> Dictionary = new ConcurrentDictionary<int, ParkingStatus>();
        public static void NullCheck(object argument, string name)
        {
            if (argument == null)
                throw new ArgumentNullException(name);
        }
        public ExitObject GetOut(int ticketId)
        {
            var currentCar = new ExitObject();
            ParkingStatus parkedCarStatusl;

            Dictionary.TryGetValue(ticketId, out parkedCarStatusl);
            Dictionary.TryRemove(ticketId, out parkedCarStatusl);

            currentCar.parkingLotID = parkedCarStatusl.parkingLotID;
            currentCar.licensePlate = parkedCarStatusl.licensePlate;

            currentCar.TotalParkTime = CalculateTime(parkedCarStatusl.StartParkingDateTime);
            currentCar.TotalPrice = CalculatePrice(currentCar.TotalParkTime);
            return currentCar;
        }
        public int CalculatePrice(int totalTimeMinutes)
        {
            return (((totalTimeMinutes / 15) * 10));
        }
        public int CalculateTime(DateTime StartTime)
        {
            var timePassed = DateTime.UtcNow.Subtract(StartTime);
            var minutes = timePassed.TotalMinutes;
            return (int)minutes;
        }


        public int GetTicketId(EntryObject licensePlateAndParkingLotID)
        {

            var currentCar = new ParkingStatus();
            int ticketId = PickValidId();
            currentCar.parkingLotID = licensePlateAndParkingLotID.parkingLotID;
            currentCar.licensePlate = licensePlateAndParkingLotID.licensePlate;
            currentCar.StartParkingDateTime = DateTime.UtcNow;
            Dictionary.TryAdd(ticketId, currentCar);
            return ticketId;
        }

        private int PickValidId()
        {
            Random r = new Random();
            int CurrentId = r.Next();
            while (Dictionary.ContainsKey(CurrentId))
            {
                CurrentId = r.Next();
            }
            return CurrentId;
        }

    }
}
