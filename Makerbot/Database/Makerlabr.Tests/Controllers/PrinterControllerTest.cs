using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Xml.XPath;
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
        private PrintersController _uut;

        [SetUp]
        public void SetUp()
        {
            _uut = new PrintersController();
        }

        [Test]
        public void IndexAction_Returns_View()
        {

            string viewName = "Index";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Index()).ViewName, "View name should have been {0}", viewName);

        }

        [Test]
        public void CreateAction_Returns_IndexViewWhenModelStateIsValid()
        {
            _uut.ModelState.Clear();

            RedirectToRouteResult result = _uut.Create(new Printer()) as RedirectToRouteResult;
            result.RouteValues["Action"].Equals("Index");
            Assert.Equals("Action", result.RouteValues["Index"]);
        }

        [Test]
        public void CreateAction_Adds_PrinterToDBWhenModelStateIsValid()
        {

            ActionResult result = _uut.Create(new Printer()) as ActionResult;
        }

        [Test]
        public void CreateAction_Returns_ViewWhenModelstateIsInvalid()
        {

            _uut.ModelState.Add("data", new ModelState());
            _uut.ModelState.AddModelError("testError", "test");  // Gør modelstate invalid. 

            string viewName = "Index";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Index()).ViewName, "View name should have been {0}", viewName);

        }

        [Test]
        public void EditAction_Returns_ViewWhenModelstateIsInvalid()
        {

            _uut.ModelState.Add("data", new ModelState());
            _uut.ModelState.AddModelError("testError", "test");  // Gør modelstate invalid. 

            string viewName = "Index";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Index()).ViewName, "View name should have been {0}", viewName);

        }

        [Test]
        public void DeleteAction_Returns_Printerview()
        {

            ActionResult result = _uut.Delete(2); //  uut modtager et ID.
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [Test]
        public void DeleteConfirmedAction_Returns_Printerview()
        {

            RedirectToRouteResult result = _uut.Create(new Printer()) as RedirectToRouteResult;
            ActionResult aresult = _uut.DeleteConfirmed(3);

            Assert.AreEqual("Index", result.RouteValues["Index"]);

        }

        [Test]
        public void DeleteConfirmedAction_Deletes_Printer()
        {
            var john = true;
            Assert.Equals("false", john);
        }

        [Test]
        public void Details_Returns_HttpBadRequestWhenIdIsNull()
        {
            ActionResult result = _uut.Details(null);
            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [Test]
        public void Details_Returns_HttpNotFoundWhenPrinterIsNull()
        {

            var printer = new Printer();
            printer.Id = 0;
            ActionResult result = _uut.Details(printer.Id);
            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

    }
}
