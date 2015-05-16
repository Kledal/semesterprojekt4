using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using FluentScheduler;
using Makerlab.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Makerlab
{
    public class MvcApplication : HttpApplication
    {
        public static ConnectionMultiplexer Redis;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
            
            try
            {
                Redis = ConnectionMultiplexer.Connect(new ConfigurationOptions()
                {
                    EndPoints = {"10.29.0.67", "6379"},
                    ServiceName = "MakerLab Redis",
                    Password = "JPFhQpvwxzSwsnJwfIHaoPgMxZJxFKO"
                });

                // Start background workers
                TaskManager.Initialize(new MyRegistry());
            } catch (Exception e)
            {
                
            }

            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
