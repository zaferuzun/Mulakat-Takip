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
        public string PanelFile { get; set; }
        public string PanelName { get; set; }
        public string PanelSurname { get; set; }
        public string PanelStatus { get; set; }
        public string PanelDefinition { get; set; }
        //public IFormFile Files { get; set; }
        public DateTime PanelDate { get; set; }

        
    }
}
