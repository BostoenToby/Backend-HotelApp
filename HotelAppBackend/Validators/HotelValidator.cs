namespace Hotels.Validators;

public class HotelValidator : AbstractValidator<Hotel>
{
    public HotelValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(3).WithMessage("The name of the hotel must be at least 3 characters long");
        RuleFor(p => p.City).NotEmpty().MinimumLength(3).WithMessage("The city must be at least 3 characters long");
        RuleFor(p => p.Address).NotEmpty().MinimumLength(5).WithMessage("The address must be at least 5 characters long");
        RuleFor(p => p.Province).NotEmpty().MinimumLength(5).WithMessage("The province must at least be 5 characters long");
        RuleFor(p => p.StarRating).NotEmpty().LessThanOrEqualTo(5).GreaterThanOrEqualTo(0).WithMessage("The star rating should be bigger or equal to 0 and less than or equal to 5.");
        RuleFor(p => p.Image).NotEmpty().WithMessage("Be sure that there is an image url entered in the body.");
        RuleFor(p => p.Longitude).NotEmpty().ScalePrecision(7, 9).WithMessage("The Longitude must have 7 digits after the decimal point and 2 digit in front of it");
        RuleFor(p => p.Latitude).NotEmpty().ScalePrecision(7, 9).WithMessage("The Latitude must have 7 digits after the decimal point and 2 digit in front of it");
        RuleFor(p => p.PricePerNightMin).NotEmpty().GreaterThan(0).ScalePrecision(2, 6).LessThan(p => p.PricePerNightMax).WithMessage("The minimum price per night must have 2 digits after the decimal point and also 2 digits in front of it");
        RuleFor(p => p.PricePerNightMax).NotEmpty().GreaterThan(0).ScalePrecision(2, 6).GreaterThan(p => p.PricePerNightMin).WithMessage("The maximum price per night must have 2 digits after the decimal point and also 2 digits in front of it");
        RuleFor(p => p.RoomTypes).NotEmpty().Must(ValidRoomTypeList).WithMessage("There need to be roomtypes that have the same properties as the model roomType.");
        When(p => p.Description != null, () => { RuleFor(p => p.Description).NotEmpty().MinimumLength(3).WithMessage("The description of the hotel must be more than 2 characters"); });
        When(p => p.Reviews != null, () => { RuleForEach(p => p.Reviews).SetValidator(new ReviewValidator());});
    }

    private bool ValidRoomTypeList(List<RoomType> roomTypes)
    {
        return !roomTypes.Equals(default(List<RoomType>));
    }
}