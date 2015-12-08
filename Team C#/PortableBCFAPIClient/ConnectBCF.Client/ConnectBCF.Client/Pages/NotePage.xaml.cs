using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectBCF.Client.ViewModels;
using Xamarin.Forms;

namespace ConnectBCF.Client.Pages
{
	public partial class NotePage : ContentPage
	{
        public NotePage(NoteViewModel viewModel)
		{
            BindingContext = viewModel;
            Title = viewModel.Note.Title;

			InitializeComponent ();
		}
	}
}
