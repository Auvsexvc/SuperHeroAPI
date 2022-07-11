using FluentValidation;

namespace SuperHeroAPI.Models.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator(SuperHeroDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).Custom((value, context) =>
            {
                var nameInUse = dbContext.Users.Any(u => u.Name == value);
                if (nameInUse)
                {
                    context.AddFailure("Name", " This user name is already in use");
                }
            });

            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e => e.Password);

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(u => u.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "This email is already in use");
                }
            });

            RuleFor(x => x.RoleId).Custom((value, context) =>
            {
                if (!dbContext.Roles.Any(r => r.Id == value))
                {
                    context.AddFailure("Role", "Provided role not found");
                }
            });
        }
    }
}
