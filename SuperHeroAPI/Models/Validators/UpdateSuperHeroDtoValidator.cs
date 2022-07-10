using FluentValidation;

namespace SuperHeroAPI.Models.Validators
{
    public class UpdateSuperHeroDtoValidator : AbstractValidator<UpdateSuperHeroDto>
    {
        public UpdateSuperHeroDtoValidator(SuperHeroDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50).Custom((value, context) =>
            {
                var nameInUse = dbContext.SuperHeroes.Any(h => h.Name == value);
                if (nameInUse)
                {
                    context.AddFailure("Name", "Provided name is already in use.");
                }
            });

            RuleFor(x => x.FirstName).MaximumLength(50);

            RuleFor(x => x.LastName).MaximumLength(100);

            RuleFor(x => x.Place).MaximumLength(150);

            RuleFor(x => x.Email).EmailAddress().NotEmpty().MaximumLength(50).Custom((value, context) =>
            {
                var emailInUse = dbContext.SuperHeroes.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "Provided email is already in use.");
                }
            });

            RuleFor(x => x.BaseStrength).NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
