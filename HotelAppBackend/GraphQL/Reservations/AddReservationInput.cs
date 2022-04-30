namespace Hotels.GraphQL.Mutations;

public record AddReservationInput(string LastName, string FirstName, string IncheckDate, string OutcheckDate, string Mail, string HotelName, double Price, string RoomTypeName);