namespace Hotels.Validators;

public class ReviewValidator : AbstractValidator<Review>
{
    public ReviewValidator()
    {
        RuleFor(p => p.Author).NotEmpty().MinimumLength(3).WithMessage("The author must at least have 3 characters");
        RuleFor(p => p.StarRating).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(5).WithMessage("The rating must be smaller or equal to 5 and must be bigger or equal to 0");
        RuleFor(p => p.ReviewDescription).NotEmpty().MinimumLength(5).WithMessage("The description must be longer than 5 characters");
    }
}