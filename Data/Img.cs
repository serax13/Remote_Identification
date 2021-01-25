using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_App.Data
{
    public class Img

    {
        public int Id { get; set; }
        [Required]
        public string PicPath1 { get; set; }
        [Required]
        public string PicPath2 { get; set; }
        [Required]
        public string PicPath3 { get; set; }

        [Required(ErrorMessage = "Загрузите фото!")]
        public byte[] Pic1 { get; set; }
        [Required(ErrorMessage = "Загрузите фото!")]
        public byte[] Pic2 { get; set; }
        [Required(ErrorMessage = "Загрузите фото!")]
        public byte[] Pic3 { get; set; }
      
        public int ClientId { get; set; }

    }
}
