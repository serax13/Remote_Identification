using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using FluentValidation;

namespace Identification.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]

        public string LastName { get; set; }
        [MaxLength(100)]
        public string MidName { get; set; }
        [MaxLength(250)]
        public string FIO { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }
        [MaxLength(20)]
        public string PassportSeries { get; set; }
        [MaxLength(20)]
        public string PassportNumber { get; set; }
        [MaxLength(20)]
        public string PassportInn { get; set; }
        [MaxLength(20)]
        public string PassportIssuedBy { get; set; }
        [MaxLength(50)]
        public string Region { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(100)]
        public string District { get; set; }
        [MaxLength(100)]
        public string Street { get; set; }
        [MaxLength(15)]
        public string HouseNumber { get; set; }
        [MaxLength(15)]
        public string AparmentNumber { get; set; }
        [MaxLength(50)]
        public string PassportRegion { get; set; }
        [MaxLength(50)]

        public string PassportCity { get; set; }
        [MaxLength(100)]

        public string PassportDistrict { get; set; }
        [MaxLength(100)]

        public string PassportStreet { get; set; }
        [MaxLength(12)]

        public string PassportHouseNumber { get; set; }
        [MaxLength(15)]
        public string PassportAparmentNumber { get; set; }
        [MaxLength(15)]

        public string MartialStatus { get; set; }
        [MaxLength(20)]

        public string Number1 { get; set; }
        [MaxLength(20)]

        public string Number2 { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime? PassportDate { get; set; }

        public DateTime? PassportEndDate { get; set; }
        [MaxLength(50)]
        public string DateTime { get; set; }
        public int Status { get; set; }
        [MaxLength(30)]
        public string Type { get; set; }
        public decimal CreditSum { get; set; }
        public int CreditTime { get; set; }
        public string Token { get; set; }

        public Img Img { get; set; }
    }

}
