public class ReservationValidator : AbstractValidator<Reservation>
{
    public ReservationValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(2).WithMessage("The name of the person must me at least 2 characters long");
        RuleFor(p => p.FirstName).NotEmpty().MinimumLength(2).WithMessage("The firstname of the person must be at least 2 characters long");
        RuleFor(p => p.BirthDate).NotEmpty().Must(ValidDate).WithMessage("The birthdate must be valid");
        RuleFor(p => p.EMail).NotEmpty().MinimumLength(7).EmailAddress().WithMessage("The email must be at least 7 characters long");
        RuleFor(p => p.Hotel).NotEmpty().Must(ValidHotel).WithMessage("The hotel must be a valid object of a hotel");
        RuleFor(p => p.NumberOfRooms).NotEmpty().GreaterThanOrEqualTo(1).WithMessage("The number of rooms must at least be 1");
        RuleFor(p => p.DateOfReservation).NotEmpty().Must(ValidDate).WithMessage("The date of reservation must be a valid date");
        RuleFor(p => p).Must(p => p.DateOfReservation >= DateTime.Now).WithMessage("The date of reservation must be in the future and not in the past");
        When(p => p.Review != null, () => {RuleFor(p => p.Review).Must(ValidReview).WithMessage("The list of reviews must be valid");});
        RuleFor(p => p.TotalPrice).NotEmpty().GreaterThan(0).WithMessage("The total price of the reservation must be greater than €0");
    }

    private bool ValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }

    private bool ValidHotel(Hotel hotel)
    {
        return !hotel.Equals(default(Hotel));
    }

    private bool ValidReview(Review review){
        return !review.Equals(default(Review));
    }
}