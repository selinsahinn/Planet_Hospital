using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace udemyWeb1.Models
{
    public class PoliklinikTuru
    {
        [Key]           // Primary Key
        public int Id { get; set; }
        [Required(ErrorMessage ="Bu Alan Boş Bırakılamaz!")]      // not Null
        [MaxLength(25)]
        [DisplayName("Poliklinik Türü Giriniz:")]
        public string Ad { get; set; }
    }
}
