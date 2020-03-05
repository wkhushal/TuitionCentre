using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWebApi.Attributes
{
    public class ToggleAuthorize : AuthorizeAttribute
    {
        public ToggleAuthorize()
            : base()
        { 
        }

        public ToggleAuthorize(bool toggleOn)
            : base()
        {
            Toggle = toggleOn;
        }

        public ToggleAuthorize(bool toggleOn, string policy)
            : base(policy)
        {
            Toggle = toggleOn;
        }

        public bool Toggle { get; private set; } = false;
    }
}
