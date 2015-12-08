using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Media;
using ConnectBCF.Shared.Common;
using PropertyChanged;
using Xamarin.Forms;

namespace ConnectBCF.Client.ViewModels
{
    [ImplementPropertyChanged]
    public class NotesViewModel : BaseViewModel
    {
        public Project Project { get; set; }

        public ObservableCollection<Note> Notes { get; set; }

        public Note SelectedNote { get; set; }
        
        public ICommand AddNoteCommand { get; private set; }
        public ICommand DeleteNoteCommand { get; private set; }

        public NotesViewModel(ConnectBCFService service, Project project) : base(service)
        {
            Project = project;

            AddNoteCommand = new Command<Note>(async (note) =>
            {
                var newNote = await Service.AddNote(Project.ProjectId, note);
                if (newNote != null)
                {
                    Notes.Add(newNote);
                    SelectedNote = newNote;
                }
            });

            DeleteNoteCommand = new Command<Note>(async (note) =>
            {
                if (await (Service.DeleteNote(Project.ProjectId, note)))
                {
                    Notes.Remove(note);
                    SelectedNote = null;
                }
            });


            Task.Run(async () =>
            {
                Notes = new ObservableCollection<Note>(await service.GetNotes(project.ProjectId));
            });
        }
    }
}
