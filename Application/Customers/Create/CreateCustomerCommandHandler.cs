using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
namespace Application.Customers.Create;

internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<Unit>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository ?? throw new ArgumentException(nameof(customerRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<Unit>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
            {
                return Error.Validation("Customer.PhoneNumber", "Phone number is not valid format. (0999-5550000)");
            }

            if (Address.Create(command.Country, command.Line1, command.Line2,
                                command.City, command.State, command.ZipCode) is not Address address)
            {
                return Error.Validation("Customer.Address", "Address is not valid");
            }

            var customer = new Customer(
                new CustomerId(Guid.NewGuid()),
                command.Name,
                command.LastName,
                command.Email,
                phoneNumber,
                address,
                true
            );

            await _customerRepository.Add(customer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            return Error.Failure("CreateCustomer.Failure", ex.Message);
        }


    }
}
