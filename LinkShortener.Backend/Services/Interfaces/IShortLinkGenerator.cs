using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Backend.Services.Interfaces
{
    public interface IShortLinkGenerator
    {
        public string Generate();
    }
}
