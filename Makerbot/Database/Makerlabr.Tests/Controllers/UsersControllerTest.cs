using System;
using System.Web.Mvc;
using Makerlab.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Makerlabr.Tests
{
    [TestClass]
    public class UsersControllerTest
    {
        [Test]
        public void DeleteAction_Returns_UsersviewResult()
        {
            var _uut = new UsersController();
            ActionResult result = _uut.Delete(2); //  uut modtager et ID.
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        
        [Test]
        public void Details_Returns_HttpBadRequestWhenIdIsNull()
        {
            var _uut = new UsersController();

            ActionResult result = _uut.Details(null);

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
    }
}
