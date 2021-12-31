using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Identification.Models;

namespace Identification.Server.Validation
{
    public class ImgValidator : AbstractValidator<Img>
    {
        public ImgValidator()
        {
            RuleFor(p => p.Pic1)
                .NotEmpty().WithMessage("Необходимо загрузить фото!");
            RuleFor(p => p.Pic2)
                .NotEmpty().WithMessage("Необходимо загрузить фото!");
            RuleFor(p => p.Pic3)
                .NotEmpty().WithMessage("Необходимо загрузить фото!");
        }
    }
}
