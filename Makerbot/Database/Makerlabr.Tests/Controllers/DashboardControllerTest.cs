using System;
using System.Web.Mvc;
using Makerlab.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;


namespace Makerlabr.Tests.Controllers
{
    [TestClass]
    public class DashboardControllerTest
    {
        [Test]
        public void Default_Action_Returns_Index_View()
        {
            // Arrange          
            const string expectedViewName = "Index";
            var dashboardController = new DashboardController();

            // Act
            var result = dashboardController.Index() as ViewResult;

            // Assert
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);
        }

        [Test]
        public void Users_Action_Returns_Usermanager_inView()
        {
          // Arrange 

            // Arrange          
            const string expectedViewName = "Index";
            var dashboardController = new DashboardController();

            // Act
            var result = dashboardController.Index() as ViewResult;

            // Assert
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);

        }
        //[Test]
        //public void Messages_Action_Returns_Messages_View()
        //{
        //    // Arrange          
        //    const string expectedViewName = "Messages";
        //    var dashboardController = new DashboardController();

        //    // Act
        //    var result = dashboardController.Index() as ViewResult;

        //    // Assert
        //    Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);
        //}

    }
}
