public class ReservationValidator : AbstractValidator<Reservation>
{
    public ReservationValidator()
    {
        RuleFor(p => p.LastName).NotEmpty().MinimumLength(2).WithMessage("The name of the person must me at least 2 characters long");
        RuleFor(p => p.FirstName).NotEmpty().MinimumLength(2).WithMessage("The firstname of the person must be at least 2 characters long");
        RuleFor(p => p.Mail).NotEmpty().MinimumLength(7).EmailAddress().WithMessage("The email must be at least 7 characters long");
        RuleFor(p => p.HotelName).NotEmpty().WithMessage("The hotel name must be a string and not empty");
        RuleFor(p => p.IncheckDate).NotEmpty().WithMessage("Make sure that there is an incheck date.");
        RuleFor(p => p.OutcheckDate).NotEmpty().WithMessage("Make sure that there is an outcheck date.");
        RuleFor(p => p.RoomTypeName).NotEmpty().MinimumLength(3).WithMessage("The roomTypeName must be valid"); 
        RuleFor(p => p.Price).NotEmpty().GreaterThan(0).WithMessage("The total price of the reservation must be greater than €0");
    }
}