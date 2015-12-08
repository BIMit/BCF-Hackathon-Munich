using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectBCF.Client.ViewModels;
using Xamarin.Forms;

namespace ConnectBCF.Client.Pages
{
	public partial class ServersPage : ContentPage
	{
		public ServersPage ()
		{
		    BindingContext = BCFAPIServer.Servers;

			InitializeComponent ();
		}

	    async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        BCFAPIServer server = (BCFAPIServer) e.SelectedItem;

	        if (server != null)
	        {
	            await Navigation.PushAsync(new LoginPage(new LoginViewModel(server)));
	            ((ListView) sender).SelectedItem = null;
	        }
	    }
	}
}
