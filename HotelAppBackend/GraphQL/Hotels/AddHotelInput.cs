namespace Hotels.GraphQL.Mutations;

public record AddHotelInput(string Name, string City, string Address, string Province, decimal Longitude, decimal Latitude, decimal PricePerNightMin, decimal PricePerNightMax, string? Description, float StarRating, string Image, List<Review>? Reviews, List<RoomType> RoomTypes);