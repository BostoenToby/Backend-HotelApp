namespace Hotels.GraphQL.Mutations;

public record AddReviewInput(string Author, double StarRating, string ReviewDescription, string Image, string HotelName);