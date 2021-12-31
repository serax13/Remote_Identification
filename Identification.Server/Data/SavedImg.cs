using System;
using System.Collections.Generic;

#nullable disable

namespace Identification.Server.Data
{
    public partial class SavedImg
    {
        public int ImgId { get; set; }
        public byte[] Pic1 { get; set; }
        public byte[] Pic2 { get; set; }
        public byte[] Pic3 { get; set; }
        public int ClientId { get; set; }

        public virtual SavedClient Client { get; set; }
    }
}
