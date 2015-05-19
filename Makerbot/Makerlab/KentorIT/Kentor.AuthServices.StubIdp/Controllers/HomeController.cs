﻿using System.Linq;
using System.Web.Mvc;
using Kentor.AuthServices.HttpModule;
using Kentor.AuthServices.Mvc;
using Kentor.AuthServices.Saml2P;
using Kentor.AuthServices.StubIdp.Models;
using Kentor.AuthServices.WebSso;

namespace Kentor.AuthServices.StubIdp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = AssertionModel.CreateFromConfiguration();

            var requestData = Request.ToHttpRequestData();
            if(requestData.QueryString["SAMLRequest"].Any())
            {
                var decodedXmlData = Saml2Binding.Get(Saml2BindingType.HttpRedirect)
                    .Unbind(requestData);

                var request = Saml2AuthenticationRequest.Read(decodedXmlData);

                model.InResponseTo = request.Id;
                model.AssertionConsumerServiceUrl = request.AssertionConsumerServiceUrl.ToString();
                model.AuthnRequestXml = decodedXmlData;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(AssertionModel model)
        {
            if (ModelState.IsValid)
            {
                var response = model.ToSaml2Response();

                var commandResult = Saml2Binding.Get(Saml2BindingType.HttpPost)
                    .Bind(response);

                return commandResult.ToActionResult();
            }

            return View(model);
        }
    }
}