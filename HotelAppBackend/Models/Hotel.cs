namespace Hotels.Models;

public class Hotel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string? Id { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Province { get; set; }
    public string? Description { get; set; }
    public float StarRating { get; set; }
    public string Image { get; set; }
    public decimal Longitude { get; set; }
    public decimal Latitude { get; set; }
    public decimal PricePerNightMin { get; set; }
    public decimal PricePerNightMax { get; set; }
    public List<Review>? Reviews { get; set; }
    public List<RoomType> RoomTypes { get; set; }
}