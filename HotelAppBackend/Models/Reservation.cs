namespace Hotels.Models;

public class Reservation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public DateTime BirthDate { get; set; }
    public string EMail { get; set; }
    public Hotel Hotel { get; set; }
    public DateTime DateOfReservation { get; set; }
    public Review? Review { get; set; }
    public double TotalPrice { get; set; }
    public RoomType RoomType { get; set; }
}
