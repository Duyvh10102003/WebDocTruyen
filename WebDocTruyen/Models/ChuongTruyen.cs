using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebDocTruyen.Models
{
    public class ChuongTruyen
    {
        public int ChuongTruyenId { get; set; }
        [Required, StringLength(150)]
        public string TenChuong { get; set; }
        public string NoiDung { get; set; }
        public int DisplayOrder { get; set; }
        public int? TruyenId { get; set; }
        public Truyen? Truyen { get; set; }
    }
}
