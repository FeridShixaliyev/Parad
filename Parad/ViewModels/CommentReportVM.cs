using Parad.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parad.ViewModels
{
    public class CommentReportVM
    {
        public AppUser AppUser { get; set; }
        public Comment Comment { get; set; }
        public CommentReport CommentReport { get; set; }
        public List<ReasonReport> ReasonReports { get; set; }
    }
}
