using System;
using System.Web.Mvc;
using Makerlab;
using Makerlab.Controllers;
using Makerlab.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NSubstitute; 
using Assert = NUnit.Framework.Assert;


namespace Makerlabr.Tests.Controllers
{
    [TestClass]
    public class DashboardControllerTest
    {
        private DashboardController _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new DashboardController();
          
        }

        [Test]
        public void Default_ActionReturns_IndexView()
        {
            string viewName = "Index";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Index()).ViewName, "View name should have been {0}", viewName);
        }

        [Test]
        public void Users_ActionReturns_UsersView()
        {
    
            string viewName = "Users";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Users()).ViewName, "View name should have been {0}", viewName);
        }

        [Test]
        public void Printers_ActionReturns_UsersView()
        {
            string viewName = "Printers";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Printers()).ViewName, "View name should have been {0}", viewName);
        }

        [Test]
        public void Userroles_ActionReturns_UsersView()
        {

            string viewName = "Userroles";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Userroles()).ViewName, "View name should have been {0}", viewName);
        }

        [Test]
        public void Messages_ActionReturns_UsersView()
        {

            string viewName = "Messages";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Messages()).ViewName, "View name should have been {0}", viewName);
        }

    }
}
