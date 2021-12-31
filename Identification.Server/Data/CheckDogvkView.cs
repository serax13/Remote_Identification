using System;
using System.Collections.Generic;

#nullable disable

namespace Identification.Server.Data
{
    public partial class CheckDogvkView
    {
        public int DvPozn { get; set; }
        public DateTime? DvDatez { get; set; }
        public int? DvKodkl { get; set; }
        public string KlInn { get; set; }
        public string KlTel1 { get; set; }
        public string KlNam { get; set; }
        public int? KlKod { get; set; }
    }
}
