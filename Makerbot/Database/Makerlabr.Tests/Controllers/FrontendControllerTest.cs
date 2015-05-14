using System;
using System.Web.Mvc;
using Makerlab.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            string viewName = "";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Index()).ViewName, "View name should have been {0}", viewName);
        }
    }
}
