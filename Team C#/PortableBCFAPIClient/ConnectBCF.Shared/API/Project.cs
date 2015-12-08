namespace ConnectBCF.Shared.API
{
    public class Project
    {
        public Project() {}

        public Project(Shared.Common.Project fromProject)
        {
            project_id = fromProject.ProjectId;
            name = fromProject.Name;
        }

        public Shared.Common.Project AsShared()
        {
            return new Shared.Common.Project() { ProjectId = project_id, Name = name };
        }

        public string project_id { get; set; }
        public string name { get; set; }
    }
}
