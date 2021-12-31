using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identification.Models
{
    public class ForRegistrationExistingClient
    {
        [MinLength(9, ErrorMessage = "Введен неккоректный формат номера!")]
        public string Number { get; set; }
        [MinLength(6, ErrorMessage = "Минимальная длина 6 символов!")]
        public string Password { get; set; }
        [MinLength(6, ErrorMessage = "Минимальная длина 6 символов!")]
        public string PasswordCheck { get; set; }
        [Required(ErrorMessage = "Выбертите тип(Кредит/Депозит)!")]
        public string TypeCode { get; set; }
        [Required(ErrorMessage = "Введите код!"), MinLength(3, ErrorMessage = "Введен неккоректный формат кода кредита или депозита!")]
        public string codeNumber { get; set; }
        //[Required(ErrorMessage = "Введите код депозита!"), MinLength(3, ErrorMessage = "Введен неккоректный формат кода кредита или депозита!")]
        //public string codeNumberDepos { get; set; }
        [Required(ErrorMessage = "Введите ИНН!"), MinLength(14, ErrorMessage = "Введен неккоректный формат!")]
        public string passportInn { get; set; }
        [MinLength(4, ErrorMessage = "Код должен состоять их 4х цифр!")]
        public string SMScode { get; set; }
    }
}
