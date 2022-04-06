namespace Hotels.GraphQL.Mutations;

public record AddRoomTypeInput(string Name, int NumberOfBeds, float SquareMeters, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View);