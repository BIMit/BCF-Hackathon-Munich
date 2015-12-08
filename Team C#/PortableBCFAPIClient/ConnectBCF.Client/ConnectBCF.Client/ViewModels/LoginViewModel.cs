using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace ConnectBCF.Client.ViewModels
{
    [ImplementPropertyChanged]
    public class LoginViewModel
    {
        public LoginViewModel(BCFAPIServer server)
        {
            Server = server;

            LoggingIn = true;
            LoggedIn = false;

            Task.Run(async () =>
            {
                using (var service = new ConnectBCFService(new Uri(Server.Uri), Server.APIVersion))
                {
                    var auth = await service.GetAuth();

                    LoginUri = auth.oauth2_auth_url + "?response_type=code&client_id=" + Server.ClientId;
                }
            });
        }

        public string LoginUri { get; set; }

        public bool LoggingIn { get; set; }
        public bool LoggedIn { get; set; }

        public BCFAPIServer Server { get; set; }
    }
}
