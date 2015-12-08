namespace ConnectBCF.Shared.API
{
    public class Topic
    {
        public Topic() {}

        public Topic(Shared.Common.Note fromNote)
        {
            title = fromNote.Title;
            guid = fromNote.NoteId;
        }

        public Shared.Common.Note AsShared()
        {
            return new Shared.Common.Note()
            {
                NoteId = guid,
                Title = title
            };
        }

        public string guid { get; set; }
        public object topic_type { get; set; }
        public object topic_status { get; set; }
        public object reference_link { get; set; }
        public string title { get; set; }
        public object priority { get; set; }
        public int? index { get; set; }
        public object labels { get; set; }
        public string creation_date { get; set; }
        public string creation_author { get; set; }
        public string modified_date { get; set; }
        public object modified_author { get; set; }
        public object assigned_to { get; set; }
        public object description { get; set; }
        public object bim_snippet { get; set; }
    }
}
