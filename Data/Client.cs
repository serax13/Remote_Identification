using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_App.Data
{
    public class Client
    {   
        [Required]
        [ValidateComplexType]
        public Img Img { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо ввести Имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Необходимо ввести Фамилию")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Необходимо ввести Отчество")]
        public string MidName { get; set; }
        [Required(ErrorMessage = "Введите номер телефона")]
        [StringLength(12,MinimumLength = 9, ErrorMessage = "Неккоректный формат номера, пример:555123123")]
        public string Number1 { get; set; }
        [Required(ErrorMessage = "Введите номер телефона WhatsApp")]
        [StringLength(12, MinimumLength = 9, ErrorMessage = "Неккоректный формат номера, пример:555123123")]
        public string Number2 { get; set; }
        public int Satus { get; set; }
        public DateTime DateTime { get; set; }
       
        public Client()
        {
            Img = new Img();
        }
    }
}
