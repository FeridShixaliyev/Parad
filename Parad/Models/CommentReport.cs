

namespace Parad.Models
{
    public class CommentReport
    {
        public int Id { get; set; }
        public string ReportContent { get; set; }
        public int ReasonReportId { get; set; }
        public ReasonReport ReasonReport { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}
