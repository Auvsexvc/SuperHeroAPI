using FluentValidation;

namespace SuperHeroAPI.Models.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3);

            RuleFor(x => x.Password).MinimumLength(6);
        }
    }
}
