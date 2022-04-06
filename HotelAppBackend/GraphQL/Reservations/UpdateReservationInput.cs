namespace Hotels.GraphQL.Mutations;

public record UpdateReservationInput(string Id, string Name, string FirstName, DateTime BirthDate, string EMail, Hotel hotel, int NumberOfRooms, DateTime DateOfReservation, Review? review, double TotalPrice);