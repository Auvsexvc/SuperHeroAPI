using FluentValidation;

namespace SuperHeroAPI.Models.Validators
{
    public class CreateSuperPowerValidator : AbstractValidator<CreateSuperPowerDto>
    {
        public CreateSuperPowerValidator(SuperHeroDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50).Custom((value, context) =>
            {
                var nameInUse = dbContext.SuperPowers.Any(p => p.Name == value);
                if (nameInUse)
                {
                    context.AddFailure("Name", "Provided name is already in use.");
                }
            });

            RuleFor(x => x.Description).MaximumLength(500);

            RuleFor(x => x.AdditionToSuperHeroStrength).NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}