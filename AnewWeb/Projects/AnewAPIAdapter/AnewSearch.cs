using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anew.AnewModels;
using System.Xml;
using System.IO;

namespace Anew.AnewAPIAdapter
{
    class AnewSearch
    {
        FlightService flightServices = new FlightService();

        public FlightSearchRs Search(FlightSearchRq rq)
        {
            DoFlightSearchRQ intermediateSearchRq = new DoFlightSearchRQ();

            intermediateSearchRq.adultPax = rq.AdultCount.ToString();
            intermediateSearchRq.childPax = rq.ChildCount.ToString();
            intermediateSearchRq.infantPax = rq.InfantCount.ToString();

            Credential credential = new Credential();
            credential.password = "123";
            credential.userId = "hyder";
            credential.userIp = "10.91.201.216";

            intermediateSearchRq.credential = credential;

            intermediateSearchRq.currency = rq.Currency;

            intermediateSearchRq.fromAirport = rq.CityPares[0].Origin;
            intermediateSearchRq.toAirport = rq.CityPares[0].Destination;

            intermediateSearchRq.fromDay = rq.CityPares[0].DepartureDateTime.Day.ToString();
            intermediateSearchRq.fromMonth = rq.CityPares[0].DepartureDateTime.Month.ToString();
            intermediateSearchRq.fromYear = rq.CityPares[0].DepartureDateTime.Year.ToString();

            if (rq.JourneyType == JourneyType.Return)
            {
                intermediateSearchRq.toDay = rq.CityPares[1].DepartureDateTime.Day.ToString();
                intermediateSearchRq.toMonth = rq.CityPares[1].DepartureDateTime.Month.ToString();
                intermediateSearchRq.toYear = rq.CityPares[1].DepartureDateTime.Year.ToString();
            }
            else
            {
                intermediateSearchRq.toDay = "";
                intermediateSearchRq.toMonth = "";
                intermediateSearchRq.toYear = "";
            }

            if (rq.JourneyType == JourneyType.OneWay)
                intermediateSearchRq.way = "1";
            else if (rq.JourneyType == JourneyType.Return)
                intermediateSearchRq.way = "2";

            DoFlightSearchRS intermediateRS = flightServices.doFlightSearch(intermediateSearchRq);

            DoGetFlightResultsRQ searchRq = new DoGetFlightResultsRQ();

            searchRq.credential = credential;

            searchRq.currency = rq.Currency;
            searchRq.searchId = intermediateRS.searchId;

            DoGetFlightResultsRS searchRS = flightServices.getFlightResults(searchRq);

            FlightSearchRs flightSearchRs = new FlightSearchRs();
            flightSearchRs.SearchRequest = rq;
            List<FlightSearchResults> FlightSearchResultsList = new List<FlightSearchResults>();
            if (searchRS != null)
            {
                for (int i = 0; i < searchRS.routerPairs.Length; i++)
                {
                    FlightSearchResults flightSearchResult = new FlightSearchResults();
                    FlightTariff flightTariff = new FlightTariff();

                    flightTariff.BaseFare = Convert.ToDouble(searchRS.routerPairs[i].amount);
                    flightTariff.Tax = Convert.ToDouble(searchRS.routerPairs[i].markupAmount) + Convert.ToDouble(searchRS.routerPairs[i].taxAmount);
                    flightTariff.TotalFare = flightTariff.BaseFare + flightTariff.Tax;

                    flightSearchResult.FlightTariff = flightTariff;

                    Itinerary itinerary = new Itinerary();
                    List<Ileg> ilegs = new List<Ileg>();

                    for (int j = 0; j < searchRS.routerPairs[i].departureRouter.segments.Length; j++)
                    {
                        Ileg ileg = new Ileg();
                        ileg.Duration = searchRS.routerPairs[i].departureRouter.segments[j].duration;
                        ileg.FlightCode = searchRS.routerPairs[i].departureRouter.segments[j].flightCode;
                        ileg.FlightNumber = searchRS.routerPairs[i].departureRouter.segments[j].flightNumber;
                        ileg.FromAirportCode = searchRS.routerPairs[i].departureRouter.segments[j].fromCode;
                        ileg.FromDateTime = Convert.ToDateTime(searchRS.routerPairs[i].departureRouter.segments[j].fromDate + " " + searchRS.routerPairs[i].departureRouter.segments[j].fromTime);
                        ileg.LegId = 1;
                        ileg.MarketingAirline = searchRS.routerPairs[i].departureRouter.segments[j].operatorCode;
                        ileg.OperationAirline = searchRS.routerPairs[i].departureRouter.segments[j].vendorCode;
                        ileg.ToAirportCode = searchRS.routerPairs[i].departureRouter.segments[j].toCode;
                        ileg.ToDateTime = Convert.ToDateTime(searchRS.routerPairs[i].departureRouter.segments[j].toDate + " " + searchRS.routerPairs[i].departureRouter.segments[j].toTime);
                        ileg.CabinClass = searchRS.routerPairs[i].departureRouter.segments[j].localClass;
                        ileg.BookingClass = searchRS.routerPairs[i].departureRouter.segments[j].supplierClass;
                        ileg.IsReturn = false;

                        ilegs.Add(ileg);
                    }
                    if (rq.JourneyType == JourneyType.Return)
                    {
                        for (int j = 0; j < searchRS.routerPairs[i].returnRouter.segments.Length; j++)
                        {
                            Ileg ileg = new Ileg();
                            ileg.Duration = searchRS.routerPairs[i].returnRouter.segments[j].duration;
                            ileg.FlightCode = searchRS.routerPairs[i].returnRouter.segments[j].flightCode;
                            ileg.FlightNumber = searchRS.routerPairs[i].returnRouter.segments[j].flightNumber;
                            ileg.FromAirportCode = searchRS.routerPairs[i].returnRouter.segments[j].fromCode;
                            ileg.FromDateTime = Convert.ToDateTime(searchRS.routerPairs[i].returnRouter.segments[j].fromDate + " " + searchRS.routerPairs[i].returnRouter.segments[j].fromTime);
                            ileg.LegId = 2;
                            ileg.MarketingAirline = searchRS.routerPairs[i].returnRouter.segments[j].operatorCode;
                            ileg.OperationAirline = searchRS.routerPairs[i].returnRouter.segments[j].vendorCode;
                            ileg.ToAirportCode = searchRS.routerPairs[i].returnRouter.segments[j].toCode;
                            ileg.ToDateTime = Convert.ToDateTime(searchRS.routerPairs[i].returnRouter.segments[j].toDate + " " + searchRS.routerPairs[i].returnRouter.segments[j].toTime);
                            ileg.CabinClass = searchRS.routerPairs[i].returnRouter.segments[j].localClass;
                            ileg.BookingClass = searchRS.routerPairs[i].returnRouter.segments[j].supplierClass;
                            ileg.IsReturn = true;

                            ilegs.Add(ileg);
                        }
                    }
                    itinerary.Ileg = ilegs;
                    flightSearchResult.Itinerary = itinerary;

                    FlightSearchResultsList.Add(flightSearchResult);
                }

                flightSearchRs.FlightSearchResults = FlightSearchResultsList;
            }
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(FlightSearchRs));

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UnicodeEncoding(false, false); // no BOM in a .NET string
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (StringWriter textWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.Serialize(xmlWriter, flightSearchRs);
                }
                string AnewSearchXML = textWriter.ToString(); //This is the output as a string
            }
            return flightSearchRs;
        }
    }
}
