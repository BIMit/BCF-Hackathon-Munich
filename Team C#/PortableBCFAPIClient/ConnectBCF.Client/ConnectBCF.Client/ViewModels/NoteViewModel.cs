using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ConnectBCF.Shared.Common;
using PropertyChanged;
using Xamarin.Forms;

namespace ConnectBCF.Client.ViewModels
{
    [ImplementPropertyChanged]
    public class NoteViewModel : BaseViewModel
    {
        public Note Note { get; private set; }
        public List<Comment> Comments { get; private set; }
        public Comment SelectedComment { get; set; }

        public string CommentText { get; set; }
        public string StatusText { get; set; }

        public ICommand AddCommentCommand { get; private set; }

        public NoteViewModel(ConnectBCFService service, Project project, Note note)
            : base(service)
        {
            Note = note;

            AddCommentCommand = new Command(async () =>
            {
                var newComment =
                    await Service.AddComment(project.ProjectId, note.NoteId, new Comment { Text = CommentText, Status = StatusText });
                if (newComment != null)
                {
                    Comments.Add(newComment);
                    SelectedComment = newComment;
                    CommentText = "";
                }
            }); // () => !string.IsNullOrEmpty(CommentText));

            Task.Run(async () =>
            {
                Comments = await service.GetComments(project.ProjectId, note.NoteId);
            });
        }
    }
}
