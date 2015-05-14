using System;
using System.Web.Mvc;
using Makerlab.Controllers;
using Makerlab.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Makerlabr.Tests.Controllers
{
    [TestClass]
    public class PrinterControllerTest
    {
        [Test]
        public void CreateAction_Return_PrinterView()
        {
            var _uut = new PrintersController();
            _uut.ModelState.Clear();

            RedirectToRouteResult result = _uut.Create(new Printer()) as RedirectToRouteResult;
            Assert.Equals("Index", result.RouteValues["Index"]);
            // ActionResult action = PrintersController.Index();

        }

        [Test]
        public void  DeleteAction_Returns_Printerview()
        {
            var _uut = new PrintersController();

            ActionResult result = _uut.Delete(2);

           // string viewName = "Index";
            //Assert.AreEqual(viewName, ((ViewResult)_uut.Index()).ViewName, "View name should have been {0}", viewName);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

    }
}
