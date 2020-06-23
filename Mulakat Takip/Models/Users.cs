using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mulakat_Takip.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(30, ErrorMessage = "{0} alanı en az {2}, en fazla {1}  karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(30, ErrorMessage = "{0} alanı en az {2}, en fazla {1} karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Geçerli mail adresi girilmedi.")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(30, ErrorMessage = "{0} alanı en az {2}, en fazla {1}  karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Adı")]
        public string UserNames { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(30, ErrorMessage = "{0} alanı en az {2}, en fazla {1}  karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Soyadı")]
        public string UserSurName { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(30, ErrorMessage = "{0} alanı en az {2}, en fazla {1} karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Telefon")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçerli telefon numarası girilmedi")]
        public string UserPhone { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(30, ErrorMessage = "{0} alanı en az {2}, en fazla {1} karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Şifre")]
        public string UserPassword { get; set; }

        public bool UserAuthorization { get; set; }
    }
}
