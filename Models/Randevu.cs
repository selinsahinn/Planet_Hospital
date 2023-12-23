using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace udemyWeb1.Models
{
    public class Randevu
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string HastaId { get; set; }

        [ValidateNever]
        public int DoktorId {  get; set; }
        [ForeignKey("DoktorId")]

        [ValidateNever]
        public Doktor Doktor { get; set; }

        [Required]
        public string RandevuTarihi { get; set; }

        [Required]
        public string RandevuSaati { get; set; }




    }
}
