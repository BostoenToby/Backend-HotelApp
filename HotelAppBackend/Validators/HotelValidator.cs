namespace Hotels.Validators;

public class HotelValidator : AbstractValidator<Hotel>
{
    public HotelValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(3).WithMessage("The name of the hotel must be at least 3 characters long");
        RuleFor(p => p.City).NotEmpty().MinimumLength(3).WithMessage("The city must be at least 3 characters long");
        RuleFor(p => p.Address).NotEmpty().MinimumLength(5).WithMessage("The address must be at least 5 characters long");
        RuleFor(p => p.Province).NotEmpty().MinimumLength(5).WithMessage("The province must at least be 5 characters long");
        RuleFor(p => p.Longitude).NotEmpty().ScalePrecision(6, 7).WithMessage("The Longitude must have 6 digits after the decimal point and 1 digit in front of it");
        RuleFor(p => p.Latitude).NotEmpty().ScalePrecision(6, 7).WithMessage("The Latitude must have 6 digits after the decimal point and 1 digit in front of it");
        RuleFor(p => p.PricePerNightMin).NotEmpty().GreaterThan(0).ScalePrecision(2, 6).LessThan(p => p.PricePerNightMax).WithMessage("The minimum price per night must have 2 digits after the decimal point and also 2 digits in front of it");
        RuleFor(p => p.PricePerNightMax).NotEmpty().GreaterThan(0).ScalePrecision(2, 6).GreaterThan(p => p.PricePerNightMin).WithMessage("The maximum price per night must have 2 digits after the decimal point and also 2 digits in front of it");
        When(p => p.Description != null, () => { RuleFor(p => p.Description).NotEmpty().MinimumLength(3).WithMessage("The description of the hotel must be more than 2 characters"); });
        When(p => p.StarRating != null, () => { RuleFor(p => p.StarRating).NotEmpty().LessThanOrEqualTo(5).GreaterThanOrEqualTo(0).WithMessage("The rating of the hotel must be less than 5 and more than 0"); });
        When(p => p.Reviews != null, () => { RuleForEach(p => p.Reviews).SetValidator(new ReviewValidator());});
    }
}