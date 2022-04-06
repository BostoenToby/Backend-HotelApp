namespace Hotels.Validators;

public class RoomTypeValidator : AbstractValidator<RoomType>
{
    public RoomTypeValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(3).WithMessage("The name must be longer than 2 characters");
        RuleFor(p => p.Television).Must(p => p == false || p == true).WithMessage("The value for television must be valid");
        RuleFor(p => p.Breakfast).Must(p => p == false || p == true).WithMessage("The value for breakfast must be valid");
        RuleFor(p => p.Airco).Must(p => p == false || p == true).WithMessage("The value for airco must be valid");
        RuleFor(p => p.Wifi).Must(p => p == false || p == true).WithMessage("The value for wifi must be valid");
        RuleFor(p => p.View).Must(p => p == false || p == true).WithMessage("The value for view must be valid");
        RuleFor(p => p.NumberOfBeds).GreaterThanOrEqualTo(1).LessThanOrEqualTo(15).WithMessage("The number of beds must be a valid number between 1 and 15");
        RuleFor(p => p.SquareMeters).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).WithMessage("The number of square meters must be a valid number between 1 and 100");
    }
}