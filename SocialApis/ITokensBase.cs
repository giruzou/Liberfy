﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApis
{
    interface ITokensBase
    {
        string ConsumerKey { get; }
        string ConsumerSecret { get; }
        string ApiToken { get; }
        string ApiTokenSecret { get; }
    }
}
