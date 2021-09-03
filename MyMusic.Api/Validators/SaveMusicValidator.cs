using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MyMusic.Api.Models.ViewModels;

namespace MyMusic.Api.Validators
{
    public class SaveMusicValidator : AbstractValidator<SaveMusicViewModel>
    {
        public SaveMusicValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(70);
            RuleFor(m => m.ArtistId)
                .NotEmpty()
                .WithMessage("Artist`s id can`t be 0");
        }
    }
}
