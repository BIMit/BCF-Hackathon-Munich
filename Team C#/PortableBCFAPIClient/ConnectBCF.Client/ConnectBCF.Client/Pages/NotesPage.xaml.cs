using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectBCF.Client.ViewModels;
using ConnectBCF.Shared.Common;
using Xamarin.Forms;

namespace ConnectBCF.Client.Pages
{
	public partial class NotesPage : ContentPage
	{
        public NotesPage(NotesViewModel viewModel)
		{
            BindingContext = viewModel;
            Title = viewModel.Project.Name;

		    InitializeComponent();
            InitializeToolbar();
		}

        private void OnNoteSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var note = (Note)e.SelectedItem;

            if (note != null)
            {
                var viewModel = (NotesViewModel) BindingContext;
                Navigation.PushAsync(
                    new NotePage(new NoteViewModel(viewModel.Service, viewModel.Project, note)));
                viewModel.SelectedNote = null;
            }
        }

        void InitializeToolbar()
        {
            var addButton = new ToolbarItem() { Text = "Add" };
            addButton.Clicked += (s, e) =>
            {
                var viewModel = (NotesViewModel)BindingContext;
                viewModel.AddNoteCommand.Execute(new Note { Title = "Test topic from mobile" });
            };
            ToolbarItems.Add(addButton);
        }

        void OnAddNote(object sender, EventArgs e)
        {

        }
	}
}
