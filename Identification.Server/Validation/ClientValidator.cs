using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using Identification.Models;

namespace Identification.Server.Validation
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleSet("Generals", () =>
            {
                RuleFor(p => p.FirstName).NotEmpty().WithMessage("Необходимо ввести имя!")
                .Matches(@"\p{IsCyrillic}").WithMessage("Допустимы только символы кириллицы");
                RuleFor(p => p.LastName).NotEmpty().WithMessage("Необходимо ввести фамилию!")
                .Matches(@"\p{IsCyrillic}").WithMessage("Допустимы только символы кириллицы");
                RuleFor(p => p.MidName).NotEmpty().WithMessage("Необходимо ввести отчество!")
                .Matches(@"\p{IsCyrillic}").WithMessage("Допустимы только символы кириллицы");
                RuleFor(p => p.Gender).NotEmpty().WithMessage("Необходимо выбрать пол!");
                RuleFor(p => p.City).NotEmpty().WithMessage("Необходимо ввести Город!")
                .Matches(@"\p{IsCyrillic}").WithMessage("Допустимы только символы кириллицы");
                RuleFor(p => p.Street).NotEmpty().WithMessage("Необходимо ввести Улицу!")
                .Matches(@"\p{IsCyrillic}").WithMessage("Допустимы только символы кириллицы");
                RuleFor(p => p.Region).NotEmpty().WithMessage("Необходимо выбрать регион!");
                RuleFor(p => p.District).NotEmpty().WithMessage("Необходимо ввести район!")
                .Matches(@"\p{IsCyrillic}").WithMessage("Допустимы только символы кириллицы");
                RuleFor(p => p.HouseNumber).NotEmpty().WithMessage("Необходимо ввести номер дома!");
                RuleFor(p => p.MartialStatus).NotEmpty().WithMessage("Необходимов выбрать семейное положение!");
                RuleFor(p => p.Birthday).NotEmpty().WithMessage("Необходимо ввести дату рождения!");
            });

            RuleSet("Passport", () =>
            {
                RuleFor(p => p.PassportSeries).NotEmpty().WithMessage("Необходимо выбрать серию!");
                RuleFor(p => p.PassportNumber).NotEmpty().WithMessage("Необходимо ввести номер паспорта!")
                .MinimumLength(7).WithMessage("Неккоректный формат!")
                .MaximumLength(7).WithMessage("Неккоректный формат!")
                 .Custom((p, context) =>
                 {
                     if ((!int.TryParse(p, out int value)))
                     {
                         context.AddFailure("Строка должна состоять из цифр");
                     }
                 });
                RuleFor(p => p.PassportInn).NotEmpty().WithMessage("Необходимо ввести инн паспорта")
                .MinimumLength(14).WithMessage("Неккоректный формат!")
                .MaximumLength(14).WithMessage("Неккоректный формат!")
                .Custom((p, context) =>
                {
                    if ((!long.TryParse(p, out long value)))
                    {
                        context.AddFailure("Строка должна состоять из цифр");
                    }
                });
                RuleFor(p => p.PassportIssuedBy).NotEmpty().WithMessage("Необходимо ввести орган выдачи паспорта")
                .MinimumLength(7).WithMessage("Неккоректный формат!")
                .MaximumLength(9).WithMessage("Неккоректный формат!");
                RuleFor(p => p.PassportDate).NotEmpty().WithMessage("Необходимо ввести дату выдачи паспорта"!);
                RuleFor(p => p.PassportEndDate).NotEmpty().WithMessage("Необходимо ввести дату окончания срока!");

            });

            RuleSet("Inn", () =>
            {
                RuleFor(p => p.PassportInn).NotEmpty().WithMessage("Необходимо ввести инн паспорта")
                .MinimumLength(14).WithMessage("Неккоректный формат!")
                .MaximumLength(14).WithMessage("Неккоректный формат!")
                .Custom((p, context) =>
                {
                    if ((!long.TryParse(p, out long value)))
                    {
                        context.AddFailure("Строка должна состоять из цифр");
                    }
                });
            });

            RuleSet("PassportAdress", () =>
            {
                RuleFor(p => p.PassportCity).NotEmpty().WithMessage("Необходимо ввести Город!")
                .Matches(@"\p{IsCyrillic}").WithMessage("Допустимы только символы кириллицы");
                RuleFor(p => p.PassportDistrict).NotEmpty().WithMessage("Необходимо ввести Район!")
                .Matches(@"\p{IsCyrillic}").WithMessage("Допустимы только символы кириллицы");
                RuleFor(p => p.PassportHouseNumber).NotEmpty().WithMessage("Необходимо ввести номер дома");
                RuleFor(p => p.PassportRegion).NotEmpty().WithMessage("Необходимо выбрать регион!");
                RuleFor(p => p.PassportStreet).NotEmpty().WithMessage("Необходимо ввести улицу!")
                .Matches(@"\p{IsCyrillic}").WithMessage("Допустимы только символы кириллицы");
            });

            RuleFor(p => p.Number1).NotEmpty().WithMessage("Необходимо ввести номер!");
            RuleFor(p => p.Number2).NotEmpty().WithMessage("Необходимо ввести номер вотсапп!");
            //RuleFor(p => p.Type).NotEmpty().WithMessage("Необходимо выбрать услугу!");
            RuleFor(p => p.Img).SetValidator(new ImgValidator());
        }
        protected bool IsValid(string name)
        {
            if (!Regex.IsMatch(name, @"\p{IsCyrillic}"))
            {
                return true;
            }
            else
                return false;

        }
    }
}
