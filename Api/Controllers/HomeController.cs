using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.Http;
using Serilog;

namespace Api.Controllers
{
    public class HomeController : ApiController
    {
        /*
         * The path is relative to the bin folder.
         * The function name has to match the function name in the C++ DLL,
         * if required you can override this via the EntryPoint property
         * in the DllImportAttribute
         */
        [DllImport("Ugly\\x64\\Ugly.dll")]
        public static extern int fnUgly();

        public IEnumerable<string> Get()
        {
            Log.Information("About to call native code!");

            var value1 = fnUgly().ToString();

            Log.Information("Called natived code!");

            return new[] { value1, "value2" };
        }
    }
}
