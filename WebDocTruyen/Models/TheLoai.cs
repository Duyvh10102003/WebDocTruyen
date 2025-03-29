using System.ComponentModel.DataAnnotations;

namespace WebDocTruyen.Models
{
    public class TheLoai
    {
        public int TheLoaiId { get; set; }
        [Required, StringLength(100)]
        public string TenTheLoai { get; set; }

        public List<Truyen>? Truyens { get; set; }

    }
}
