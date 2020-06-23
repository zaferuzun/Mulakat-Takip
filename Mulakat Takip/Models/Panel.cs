using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mulakat_Takip.Models
{
    public class PanelOperations
    {
        [Key]
        public int Panelid { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Dosya")]
        public string PanelFile { get; set; }

        [StringLength(30, ErrorMessage = "{0} alanı en az {2}, en fazla {1}  karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Adı")]
        public string PanelName { get; set; }

        [StringLength(30, ErrorMessage = "{0} alanı en az {2}, en fazla {1}  karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Soyadı")]
        public string PanelSurname { get; set; }

        [Display(Name = "Sonuç")]
        public string PanelStatus { get; set; }

        [StringLength(30, ErrorMessage = "{0} alanı en az {2}, en fazla {1}  karakter uzunluğunda olmalıdır!", MinimumLength = 2)]
        [Display(Name = "Açıklama")]
        public string PanelDefinition { get; set; }
        //public IFormFile Files { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Başvuru Tarihi")]
        [DataType(DataType.Date,ErrorMessage ="Lütfen geçerli bir tarih giriniz.")]
        public DateTime PanelDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sonuç Tarihi")]
        public DateTime? PanelPostDate { get; set; }


    }
}
