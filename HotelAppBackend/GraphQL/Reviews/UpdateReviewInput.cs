namespace Hotels.GraphQL.Mutations;

public record UpdateReviewInput(string Id, string Author, double StarRating, string ReviewDescription, string Image);