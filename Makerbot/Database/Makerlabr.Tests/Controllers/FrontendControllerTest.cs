using System.Web.Mvc;
using Makerlab.Controllers;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Makerlabr.Tests.Controllers
{
    [TestFixture]
    public class FrontendControllerTest
    {
        private FrontendController _uut;

        [SetUp]
        public void SetUp()
        {
           _uut = new FrontendController();
        }
        
        [Test]
        public void Logind_Returns_View()
        {
            string viewName = "LogInd";
            Assert.AreEqual(viewName, ((ViewResult)_uut.LogInd()).ViewName, "View name should have been {0}", viewName);
        }

        [Test]
        public void NewBookings_Returns_View()
        {
            string viewName = "NewBooking";
            Assert.AreEqual(viewName, ((ViewResult)_uut.NewBooking()).ViewName, "View name should have been {0}", viewName);
        }

        [Test]
        public void MyBookings_Returns_View()
        {

            var abe = false; 

            Assert.IsTrue(abe);
        }
    }
}
