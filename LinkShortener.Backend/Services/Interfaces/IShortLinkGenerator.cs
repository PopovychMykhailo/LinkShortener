﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Resource.Services.Interfaces
{
    public interface IShortLinkGenerator
    {
        public string Generate();
    }
}
