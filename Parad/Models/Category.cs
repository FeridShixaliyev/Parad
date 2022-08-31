using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parad.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string DescriptionImage { get; set; }
        [NotMapped]
        public IFormFile DescriptionImageFile { get; set; }
        public List<Image> Images { get; set; }
    }
}
