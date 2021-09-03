using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MyMusic.Api.Models.ViewModels;

namespace MyMusic.Api.Validators
{
    public class SaveArtistValidator : AbstractValidator<SaveArtistViewModel>
    {
        public SaveArtistValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
