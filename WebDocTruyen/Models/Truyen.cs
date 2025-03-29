using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebDocTruyen.Models
{
    public class Truyen
    {
        public int TruyenId { get; set; }
        [Required, StringLength(150)]
        public string TenTruyen { get; set; }
        public string NoiDung { get; set; }
        public string? Anh { get; set; }
        public int? TacGiaId { get; set; }
        public int? TheLoaiId { get; set; }
        public int? TrangThaiId { get; set; }
        public TacGia? TacGia { get; set; }
        public TheLoai? TheLoai {  get; set; }
        public TrangThai? TrangThai { get; set; }
        public List<ChuongTruyen>? ChuongTruyens { get; set; }
    }
}
