using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parad.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string ImageStr { get; set; }
        public AppUser User { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public DateTime DownloadDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<TagImage> TagImages { get; set; }
    }
}
