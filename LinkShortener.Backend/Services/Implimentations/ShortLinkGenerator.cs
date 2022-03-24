using System;
using LinkShortener.Resource.Services.Interfaces;

namespace LinkShortener.Resource.Services.Implimentations
{
    public class ShortLinkGenerator : IShortLinkGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
