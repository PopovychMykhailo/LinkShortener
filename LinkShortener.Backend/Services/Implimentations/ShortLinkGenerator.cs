using System;
using LinkShortener.Backend.Services.Interfaces;

namespace LinkShortener.Backend.Services.Implimentations
{
    public class ShortLinkGenerator : IShortLinkGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
