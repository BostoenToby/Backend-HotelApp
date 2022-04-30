namespace Hotels.Models;

public class RoomType
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }
    public string Name { get; set; }
    public int NumberOfBeds { get; set; } 
    public float SquareMeters { get; set; }
    public bool Television { get; set; } //stel television = true --> alleen roomtypes met television, maar als television = false --> alle roomtypes zowel waar true als false
    public bool Breakfast { get; set; }
    public bool Airco { get; set; }
    public bool Wifi { get; set; }
    public bool View { get; set; }
    public double Price { get; set; }
    public string HotelName { get; set; }
    public string Image { get; set; }
}