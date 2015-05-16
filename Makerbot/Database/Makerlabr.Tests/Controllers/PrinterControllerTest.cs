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
        public void IndexAction_Returns_View()
        {
            var _uut = new PrintersController();
           
            string viewName = "Index";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Index()).ViewName, "View name should have been {0}", viewName);

        }
        
        [Test]
        public void CreateAction_Returns_IndexViewWhenModelStateIsValid()
        {

            var _uut = new PrintersController();  
            
            _uut.ModelState.Clear();
           
            RedirectToRouteResult result = _uut.Create(new Printer()) as RedirectToRouteResult;
            result.RouteValues["Action"].Equals("Index");
            Assert.Equals("Action", result.RouteValues["Index"]);

        }

        [Test]
        public void CreateAction_Returns_ViewWhenModelstateIsInvalid()
        {
            var _uut = new PrintersController();
            _uut.ModelState.Add("data", new ModelState());
            _uut.ModelState.AddModelError("testError", "test");  // Gør modelstate invalid. 

            string viewName = "";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Index()).ViewName, "View name should have been {0}", viewName);

        }

        [Test]
        public void EditAction_Returns_ViewWhenModelstateIsInvalid()
        {
            var _uut = new PrintersController();
            _uut.ModelState.Add("data", new ModelState());
            _uut.ModelState.AddModelError("testError", "test");  // Gør modelstate invalid. 

            string viewName = "";
            Assert.AreEqual(viewName, ((ViewResult)_uut.Index()).ViewName, "View name should have been {0}", viewName);

        }

        [Test]
        public void  DeleteAction_Returns_Printerview()
        {
            var _uut = new PrintersController();
            ActionResult result = _uut.Delete(2); //  uut modtager et ID.
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [Test]
        public void DeleteConfirmedAction_Returns_Printerview()
        {
            var _uut = new PrintersController();

            RedirectToRouteResult result = _uut.Create(new Printer()) as RedirectToRouteResult;
            ActionResult aresult = _uut.DeleteConfirmed(3); 

            Assert.Equals("Index", result.RouteValues["Index"]);
          
        }

        [Test]
        public void Details_Returns_HttpBadRequestWhenIdIsNull()
        {
            var _uut = new PrintersController();
            
            ActionResult  result = _uut.Details(null);

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }


        [Test]
        public void Details_Returns_HttpNotFoundWhenPrinterIsNull()
        {
           var printer = new Printer();
           printer.Id = 0; 
           var _uut = new PrintersController();

            ActionResult result = _uut.Details(null);

           Assert.Equals("HttpNotFound",result);
        }

    }
}
