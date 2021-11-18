using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarApi.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MadeBy { get; set; }
        public string ImageUrl { get; set; }
        public string AudioUrl { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        [NotMapped]
        public IFormFile AudioFile { get; set; }
       
    }
}
