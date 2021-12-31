using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Identification.Models
{
    public class Img
    {
        public int ImgId { get; set; }
        
        public byte[] Pic1 { get; set; }
       
        public byte[] Pic2 { get; set; }
        public byte[] Pic3 { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }

}
