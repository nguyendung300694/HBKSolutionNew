using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HBKSolution.Models
{
    public class EMailViewModel
    {
        [StringLength(100, ErrorMessage = "Nội dung quá dài")]
        [Required(ErrorMessage = "Vui lòng nhập tên của bạn")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email của bạn")]
        [StringLength(100, ErrorMessage = "Nội dung quá dài")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập nội dung email")]
        [StringLength(4000, ErrorMessage = "Nội dung quá dài")]
        public string EmailContent { get; set; }
    }
}