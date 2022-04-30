namespace Hotels.GraphQL.Mutations;

public record UpdateRoomTypeInput(string Id, string Name, int NumberOfBeds, float SquareMeters, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View, double Price, string HotelName, string Image);