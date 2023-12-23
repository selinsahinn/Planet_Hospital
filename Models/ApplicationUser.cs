using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace udemyWeb1.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public int HastaTcno {  get; set; }

        public string? Adres {  get; set; }
        public string? Cinsiyet { get; set; }
        public int? Yas { get; set; }


    }
}
