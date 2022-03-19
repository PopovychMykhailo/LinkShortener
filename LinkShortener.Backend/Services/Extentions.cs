using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Backend.Services
{
    public static class Extentions
    {
        public static string CutController(this string controllerName)
        {
            return controllerName.Replace("Controller", "");
        }
    }
}
