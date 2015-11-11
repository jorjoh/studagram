using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudaGram.Models
{
    public class UploadModel
    {

        [Required]
        [MinLength(2)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

   
        [DataType(DataType.Upload)]
        public HttpPostedFileBase UploadedFile { get; set; }

    }
}