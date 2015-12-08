using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Test.Suitebuilder;
using ConnectBCF.Client.ViewModels;
using ConnectBCF.Shared.Common;
using Xamarin.Forms;

namespace ConnectBCF.Client.Pages
{
	public partial class ProjectsPage : ContentPage
	{
		public ProjectsPage (ProjectsViewModel viewModel)
		{
            BindingContext = viewModel;

		    InitializeComponent();
		    InitializeToolbar();
		}

	    private void OnProjectSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        var project = (Project)e.SelectedItem;

	        if (project != null)
	        {
	            var viewModel = (ProjectsViewModel) BindingContext;
	            
	            Navigation.PushAsync(new NotesPage(new NotesViewModel(viewModel.Service, project)));
	            viewModel.SelectedProject = null;
	        }
	    }

        void InitializeToolbar()
        {
            var addButton = new ToolbarItem() { Text = "Add" };
            addButton.Clicked += (s,  e) =>
            {
                var viewModel = (ProjectsViewModel)BindingContext;
                viewModel.AddProjectCommand.Execute(new Project { Name = "Test project from mobile"});
            };
            ToolbarItems.Add(addButton);
        }
	}
}
