using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBKSolution.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public int ProductCategoryId { get; set; }

        [Required, StringLength(200)]
        public string ProductName { get; set; }

        [StringLength(4000)]
        public string ProductContent { get; set; }

        [Required]
        public HttpPostedFileBase FileUpload { get; set; }

        [DefaultValue(0)]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; } = 0;

        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public int View { get; set; } = 0;

        public bool IsAdd { get; set; }
    }
}