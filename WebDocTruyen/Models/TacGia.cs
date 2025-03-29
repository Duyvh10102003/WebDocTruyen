using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDocTruyen.Models
{
    public class TacGia
    {
        public int TacGiaId { get; set; }
        [Required, StringLength(100)]
        public string TenTacGia { get; set; }

        public List<Truyen>? Truyens { get; set; }
    }
}
