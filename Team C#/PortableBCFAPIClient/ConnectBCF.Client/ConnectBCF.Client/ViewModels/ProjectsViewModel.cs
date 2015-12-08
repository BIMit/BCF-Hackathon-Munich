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
    public class ProjectsViewModel : BaseViewModel
    {
        public ObservableCollection<Project> Projects { get; set; }

        public Project SelectedProject { get; set; }

        public ICommand AddProjectCommand { get; private set; }
        public ICommand DeleteProjectCommand { get; private set; }

        public ProjectsViewModel(ConnectBCFService service) : base(service)
        {
            AddProjectCommand = new Command<Project>(async (project) =>
            {
                var newProject = await Service.AddProject(project);
                if (newProject != null)
                {
                    Projects.Add(newProject);
                    SelectedProject = newProject;
                }
            });

            DeleteProjectCommand = new Command<Project>(async (project) =>
            {
                if (await (Service.DeleteProject(project)))
                {
                    Projects.Remove(project);
                    SelectedProject = null;
                }
            });

            Task.Run(async () =>
            {
                Projects = new ObservableCollection<Project>(await service.GetProjects());
            });
        }
    }
}
