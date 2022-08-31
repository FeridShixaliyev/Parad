

using System.Collections.Generic;

namespace Parad.Models
{
    public class ReasonReport
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public List<CommentReport> CommentReports { get; set; }
    }
}
