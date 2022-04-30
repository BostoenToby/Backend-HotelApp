namespace Hotels.GraphQL.Mutations;

public record UpdateReservationInput(string Id, string LastName, string FirstName, string Mail, string HotelName, string IncheckDate, string OutcheckDate, double Price, string RoomTypeName );