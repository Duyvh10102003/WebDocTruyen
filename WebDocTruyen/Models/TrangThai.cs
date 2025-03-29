using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebDocTruyen.Models
{
    public class TrangThai
    {
        public int TrangThaiId { get; set; }
        [Required, StringLength(100)]
        public string TrangThaiTruyen { get; set; }
        public List<Truyen>? Truyens { get; set; }
    }
}
