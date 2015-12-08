using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectBCF.Client.Pages;
using Xamarin.Forms;

namespace ConnectBCF.Client
{
	public class App : Application
	{
        public ConnectBCFService Service { get; set; }

		public App ()
		{
			// The root page of your application
		    MainPage = new NavigationPage(new ServersPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
