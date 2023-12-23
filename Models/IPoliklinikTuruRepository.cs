namespace udemyWeb1.Models
{
    public interface IPoliklinikTuruRepository : IRepository<PoliklinikTuru>
    {
        void Guncelle(PoliklinikTuru poliklinikTuru);
        void Kaydet();
    }
}
