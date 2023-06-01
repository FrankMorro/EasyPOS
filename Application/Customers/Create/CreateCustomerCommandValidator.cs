using FluentValidation;

namespace Application.Customers.Create;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(r => r.LastName)
            .NotEmpty()
            .MaximumLength(50)
            .WithName("Last Name");

        RuleFor(r => r.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(150);

        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .MaximumLength(12)
            .WithName("Phone Number");

        RuleFor(r => r.Country)
            .NotEmpty()
            .MaximumLength(3);

        RuleFor(r => r.Line1)
            .NotEmpty()
            .MaximumLength(50)
            .WithName("Address Line 1");

        RuleFor(r => r.Line2)
            .MaximumLength(50)
            .WithName("Address Line 2");

        RuleFor(r => r.City)
            .NotEmpty()
            .MaximumLength(40);

        RuleFor(r => r.State)
            .NotEmpty()
            .MaximumLength(40);

        RuleFor(r => r.ZipCode)
            .NotEmpty()
            .MaximumLength(10)
            .WithName("Zip Code");
    }
}
