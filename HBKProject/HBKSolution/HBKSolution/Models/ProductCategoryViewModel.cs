using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBKSolution.Models
{
    public class ProductCategoryViewModel
    {
        public int ProductCategoryId { get; set; }

        [Required, StringLength(200)]
        public string ProductCategoryName { get; set; }

        [Required]
        public HttpPostedFileBase FileUpload { get; set; }

        [Required,StringLength(100)]
        public string FolderName { get; set; }

        public bool IsAdd { get; set; }
    }
}