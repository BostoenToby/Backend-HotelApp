namespace Hotels.Models;

public class Reservation
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Mail { get; set; }
    public string HotelName { get; set; }
    public string IncheckDate { get; set; }
    public string OutcheckDate { get; set; }
    public double Price { get; set; }
    public string RoomTypeName { get; set; }
}
