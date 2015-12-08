namespace ConnectBCF.Shared.API
{
    public class Comment
    {
        public Comment() {}

        public Comment(Shared.Common.Comment fromComment)
        {
            guid = fromComment.CommentId;
            comment = fromComment.Text;
            status = fromComment.Status;
        }

        public Shared.Common.Comment AsShared()
        {
            return new Shared.Common.Comment()
            {
                CommentId = guid,
                Text = comment,
                Status = status
            };
        }

        public string guid { get; set; }
        public object verbal_status { get; set; }
        public string status { get; set; }
        public string date { get; set; }
        public string author { get; set; }
        public string comment { get; set; }
        public string topic_guid { get; set; }
        public object viewpoint_guid { get; set; }
        public object reply_to_comment_guid { get; set; }
        public string modified_date { get; set; }
        public object modified_author { get; set; }
    }
}
