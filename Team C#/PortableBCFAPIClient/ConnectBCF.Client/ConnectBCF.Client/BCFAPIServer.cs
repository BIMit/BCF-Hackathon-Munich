using System;
using System.Collections.Generic;
using System.Text;

namespace ConnectBCF.Client
{
    public class BCFAPIServer
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string APIVersion { get; set; }

        public string ClientId;
        public string ClientSecret;
        public string Scope;

        public string Details
        {
            get { return Uri + ", version " + APIVersion; }
        }

        public static List<BCFAPIServer> Servers = new List<BCFAPIServer>
        {
            new BCFAPIServer { Name = "http://bim--it-dev.iabi.biz/", Uri = "http://bim--it-dev.iabi.biz/bcf", APIVersion = "1.0", ClientId = "YjBkY2U1OTctZTY1MC00ODgwLTgxNWUtMTFlNGYwMDMwMGJh", ClientSecret = "NThmNTUzMTgtMTJhYi00MTgzLTk2ZjYtNDIzYWZmNDAzNjM1"}
        };
    }
}
