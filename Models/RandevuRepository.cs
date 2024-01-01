using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using udemyWeb1.Haberlesme;

namespace udemyWeb1.Models
{
    public class RandevuRepository : Repository<Randevu>, IRandevuRepository
    {
        private UygulamaDbContext _uygulamaDbContext;
        public RandevuRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
            _uygulamaDbContext = uygulamaDbContext;
        }

        public void Guncelle(Randevu randevu)
        {
            var existingRandevu = _uygulamaDbContext.Randevular.Find(randevu.Id);
            if (existingRandevu != null)
            {
                _uygulamaDbContext.Entry(existingRandevu).State = EntityState.Detached;
            }
            _uygulamaDbContext.Update(randevu);

        }

        public void Kaydet()
        {
            _uygulamaDbContext.SaveChanges();
        }

        public bool Any(Expression<Func<Randevu, bool>> predicate)
        {
            return _uygulamaDbContext.Randevular.Any(predicate);
        }
    }
}
