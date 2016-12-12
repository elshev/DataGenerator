using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(APaers.DataGen.WebApi.Startup))]

namespace APaers.DataGen.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureContainer(app);
            ConfigureAuth(app);
        }
    }
}
