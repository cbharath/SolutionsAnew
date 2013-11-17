using Anew.AnewAPIAdapter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Anew.AnewModels;
using System.Collections.Generic;

namespace AnewAPIAdapterTest
{
    
    
    /// <summary>
    ///This is a test class for AnewSearchTest and is intended
    ///to contain all AnewSearchTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AnewSearchTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for Search
        ///</summary>
        [TestMethod()]
        public void SearchTest1()
        {
            AnewSearch target = new AnewSearch(); // TODO: Initialize to an appropriate value
            FlightSearchRq rq = new FlightSearchRq(); // TODO: Initialize to an appropriate value

            rq.AdultCount = 1;
            rq.ChildCount = 1;
            rq.InfantCount = 1;
            List<CityPair> cityPares = new List<CityPair>();

            CityPair cityPair = new CityPair();

            DateTime dt = new DateTime(2014,1,4);
            cityPair.DepartureDateTime = dt;
            cityPair.Origin = "SIN";
            cityPair.Destination = "MNL";
            cityPares.Add(cityPair);

            //cityPair = new CityPair();
            //DateTime dt1 = new DateTime(2014, 1, 14);
            //cityPair.DepartureDateTime = dt1;
            //cityPair.Origin = "MNL";
            //cityPair.Destination = "SIN";

            //cityPares.Add(cityPair);
            
            rq.CityPares = cityPares;
            rq.Currency = "USD";
            rq.JourneyType = JourneyType.OneWay;
            
            FlightSearchRs expected = null; // TODO: Initialize to an appropriate value
            FlightSearchRs actual;
            actual = target.Search(rq);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
