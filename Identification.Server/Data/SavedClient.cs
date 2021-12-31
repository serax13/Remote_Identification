using System;
using System.Collections.Generic;

#nullable disable

namespace Identification.Server.Data
{
    public partial class SavedClient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidName { get; set; }
        public string Fio { get; set; }
        public string Gender { get; set; }
        public string PassportSeries { get; set; }
        public string PassportNumber { get; set; }
        public string PassportInn { get; set; }
        public string PassportIssuedBy { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string AparmentNumber { get; set; }
        public string PassportRegion { get; set; }
        public string PassportCity { get; set; }
        public string PassportDistrict { get; set; }
        public string PassportStreet { get; set; }
        public string PassportHouseNumber { get; set; }
        public string PassportAparmentNumber { get; set; }
        public string MartialStatus { get; set; }
        public string Number1 { get; set; }
        public string Number2 { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? PassportDate { get; set; }
        public DateTime? PassportEndDate { get; set; }
        public string DateTime { get; set; }
        public int Status { get; set; }
        public string Type { get; set; }
        public decimal CreditSum { get; set; }
        public int CreditTime { get; set; }
        public string Token { get; set; }

        public virtual SavedImg SavedImg { get; set; }
    }
}
