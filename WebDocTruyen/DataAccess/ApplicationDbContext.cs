using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebDocTruyen.Models;

namespace WebDocTruyen.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<Truyen> Truyens { get; set; }
        public DbSet<TheLoai> TheLoais { get; set; }
        public DbSet<TacGia> TacGias { get; set; }
        public DbSet<TrangThai> TrangThais { get; set; }
        public DbSet<ChuongTruyen> ChuongTruyens { get; set; }

    }
}
