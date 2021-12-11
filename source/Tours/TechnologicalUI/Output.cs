using System;
using Tours;

namespace TechnologicalUI
{
    public class Output
    {
        public void outputTour(Tour tour)
        {
            Console.WriteLine("TourID = " + Convert.ToString(tour.Tourid) +
                                "\tHotelID = " + Convert.ToString(tour.Hotel) +
                                "\tFoodID = " + Convert.ToString(tour.Food) +
                                "\tTransferID = " + Convert.ToString(tour.Transfer) +
                                "\tCost = " + Convert.ToString(tour.Cost) +
                                "\tDateBegin = " + Convert.ToString(tour.Datebegin) +
                                "\tDateEnd = " + Convert.ToString(tour.Dateend));
        }

        public void outputHotel(Hotel hotel)
        {
            Console.WriteLine("HotelID = " + Convert.ToString(hotel.Hotelid) +
                                "\tCity = " + Convert.ToString(hotel.City) +
                                "\tName = " + Convert.ToString(hotel.Name) +
                                "\tType = " + Convert.ToString(hotel.Type) +
                                "\tClass = " + Convert.ToString(hotel.Class) +
                                "\tSwimPool = " + Convert.ToString(hotel.Swimpool) +
                                "\tCost = " + Convert.ToString(hotel.Cost));
        }

        public void outputFood(Food food)
        {
            Console.WriteLine("FoodID = " + Convert.ToString(food.Foodid) +
                                "\tCategory = " + Convert.ToString(food.Category) +
                                "\tVegMenu = " + Convert.ToString(food.Vegmenu) +
                                "\tChildrenMenu = " + Convert.ToString(food.Childrenmenu) +
                                "\tBar = " + Convert.ToString(food.Bar) +
                                "\tCost = " + Convert.ToString(food.Cost));
        }

        public void outputTransfer(Transfer transfer)
        {
            Console.WriteLine("TransferID = " + Convert.ToString(transfer.Transferid) +
                                "\tPlane = " + Convert.ToString(transfer.Planeticket) +
                                "\tTrain = " + Convert.ToString(transfer.Trainticket) +
                                "\tBus = " + Convert.ToString(transfer.Busticket));
        }

        public void outputBus(Busticket bus)
        {
            Console.WriteLine("BusID = " + Convert.ToString(bus.Bustid) +
                                "\tBus = " + Convert.ToString(bus.Bus) +
                                "\tSeat = " + Convert.ToString(bus.Seat) +
                                "\tCityfrom = " + Convert.ToString(bus.Cityfrom) +
                                "\tCityto = " + Convert.ToString(bus.Cityto) +
                                "\tDeparturetime = " + Convert.ToString(bus.Departuretime) +
                                "\tArrivaltime = " + Convert.ToString(bus.Arrivaltime) +
                                "\tLuggage = " + Convert.ToString(bus.Luggage) +
                                "\tCost = " + Convert.ToString(bus.Cost));
        }

        public void outputUser(User user)
        {
            Console.WriteLine("UserID = " + Convert.ToString(user.Userid) +
                                "\tName = " + Convert.ToString(user.Name) +
                                "\tSurname = " + Convert.ToString(user.Surname) +
                                "\tYear = " + Convert.ToString(user.Year));
        }
    }
}
