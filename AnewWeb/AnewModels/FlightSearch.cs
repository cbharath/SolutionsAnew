using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Anew.AnewModels
{
    public class FlightSearchRq
    {
        public List<CityPair> CityPares { get; set; }

        [Display(Name = "Journey Type")]
        public JourneyType JourneyType { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "Adult Count")]
        public int AdultCount { get; set; }

        [Display(Name = "Child Count")]
        public int ChildCount { get; set; }

        [Display(Name = "Infant Count")]
        public int InfantCount { get; set; }
    }
    public class FlightSearchRs
    {
        public List<FlightSearchResults> FlightSearchResults { get; set; }
        public FlightSearchRq SearchRequest { get; set; }
    }

    public class FlightSearchResults
    {
        public FlightTariff FlightTariff { get; set; }
        public Itinerary Itinerary { get; set; }
    }
    public class FlightTariff
    {
        public double BaseFare { get; set; }
        public double Tax { get; set; }
        public double TotalFare { get; set; }
    }
    public class Itinerary
    {
        public List<Ileg> Ileg { get; set; }
    }
    public class Ileg
    {
        public int LegId { get; set; }
        public string Duration { get; set; }
        public string FlightCode { get; set; }
        public string FlightNumber { get; set; }
        public string FromAirportCode { get; set; }
        public DateTime FromDateTime { get; set; }
        public string OperationAirline { get; set; }
        public string MarketingAirline { get; set; }
        public string ToAirportCode { get; set; }
        public DateTime ToDateTime { get; set; }
        public string CabinClass { get; set; }
        public string BookingClass { get; set; }
        public bool IsReturn { get; set; }
    }
    public class CityPair
    {
        [Display(Name = "Origin City")]
        public string Origin { get; set; }

        [Display(Name = "Destination City")]
        public string Destination { get; set; }


        public DateTime DepartureDateTime { get; set; }
    }
    public enum JourneyType
    {
        OneWay = 1,
        Return = 2,
        Multidestination = 3
    }
}
