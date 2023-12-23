namespace udemyWeb1.Models
{
    public interface IRandevuRepository : IRepository<Randevu>
    {
        void Guncelle(Randevu randevu);
        void Kaydet();
    }
}
