using udemyWeb1.Haberlesme;

namespace udemyWeb1.Models
{
    public class PoliklinikTuruRepository : Repository<PoliklinikTuru>, IPoliklinikTuruRepository
    {
        private UygulamaDbContext _uygulamaDbContext;
        public PoliklinikTuruRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(PoliklinikTuru poliklinikTuru)
        {
            _uygulamaDbContext.Update(poliklinikTuru);
        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }
    }
}
