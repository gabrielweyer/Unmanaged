using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Serilog;

namespace Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            const string defaultLogPath = "D:\\home\\LogFiles\\unmanaged-{Date}.txt";

            var logPath = ConfigurationManager.AppSettings["LogPath"] ?? defaultLogPath;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.RollingFile(logPath)
                .CreateLogger();

            Log.Information("Starting web app.");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //LoadNativeAssemblies();
        }

        private void LoadNativeAssemblies()
        {
            Log.Information("Loading native assemblies from {RootApplicationPath}.", Server.MapPath("~/bin"));

            try
            {
                Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));
            }
            catch (Exception ex)
            {
                Log.Error(ex, string.Empty);
                throw;
            }
        }
    }
}
