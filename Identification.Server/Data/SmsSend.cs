using System;
using System.Collections.Generic;

#nullable disable

namespace Identification.Server.Data
{
    public partial class SmsSend
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Sms { get; set; }
        public int? Status { get; set; }
        public DateTime? Datel { get; set; }
        public DateTime? DateSms { get; set; }
        public string ReturnMessage { get; set; }
        public int? RetrunStatus { get; set; }
        public int? KlKod { get; set; }
        public int? Rass { get; set; }
        public DateTime? DoDate { get; set; }
        public int? DoPozn { get; set; }
        public int? TipS { get; set; }
    }
}
