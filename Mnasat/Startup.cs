using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.UI.WebControls;

[assembly: OwinStartupAttribute(typeof(Mnasat.Startup))]
namespace Mnasat
{
    public partial class Startup
    {
        public static String GloboApiUrl = "http://localhost:16929/api/";
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        
    }
}
