using System;
using ConnectBCF.Client.ViewModels;
using RestSharp.Extensions.MonoHttp;
using Xamarin.Forms;

namespace ConnectBCF.Client.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel viewModel)
        {
            BindingContext = viewModel;
            Title = viewModel.Server.Name;

            InitializeComponent();

            LoginWebView.Navigating += async (sender, args) =>
            {
                var urlString = args.Url.ToString();
                Console.WriteLine("Navigating to: {0}", urlString);

                if (urlString.StartsWith("http://localhost"))
                {
                    string accessCode = GetParam(urlString, "code");
                    Console.WriteLine("Accesscode: {0}", accessCode);

                    using (
                        var service = new ConnectBCFService(new Uri(viewModel.Server.Uri), viewModel.Server.APIVersion))
                    {
                        var token = await service.GetToken(viewModel.Server, accessCode);

                        Console.WriteLine("Token: {0}", token.access_token);

                        viewModel.LoggingIn = false;
                        viewModel.LoggedIn = true;

                        await Navigation.PushAsync(new ProjectsPage(new ProjectsViewModel(new ConnectBCFService(new Uri(viewModel.Server.Uri), viewModel.Server.APIVersion, token.access_token))));
                    }
                }
            };
        }

        private string GetParam(string uri, string param)
        {
            uri = uri.Substring(uri.IndexOf('?') + 1);
            foreach (string item in uri.Split('&'))
            {
                string[] parts = item.Split('=');
                if (parts[0] == param)
                {
                    return parts[1];
                }
            }

            return null;
        }
    }
}
