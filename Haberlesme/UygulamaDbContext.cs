using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using udemyWeb1.Models;

//Veri tabanında EF tablo olusturmasi icin ilgili model sınıflarımızı buraya eklemeliyiz
namespace udemyWeb1.Haberlesme
{
    public class UygulamaDbContext : IdentityDbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<PoliklinikTuru> PoliklinikTurleri { get; set; }
        
        public DbSet<Doktor> Doktorlar {  get; set; }

        public DbSet<Randevu> Randevular { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
