using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mulakat_Takip.Models
{
    public class UsersLogin
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(30, ErrorMessage = "{0} alanı en az {2}, en fazla {1}  karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [StringLength(30, ErrorMessage = "{0} alanı en az {2}, en fazla {1} karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Şifre")]
        public string UserPassword { get; set; }
    }
}
