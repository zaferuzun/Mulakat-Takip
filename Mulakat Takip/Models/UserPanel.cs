using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mulakat_Takip.Models
{
    public class UserPanel
    {
        [Key]
        public int Panelid { get; set; }
        public int UserId { get; set; }
        public string PanelName { get; set; }
        public string PanelSurname { get; set; }
        public string PanelFile { get; set; }
        public DateTime PanelDate { get; set; }
        public DateTime PanelPostDate { get; set; }
        

    }
}
