namespace Mongo.Context;

public interface IMongoContext
{
    IMongoClient Client { get; }
    IMongoDatabase Database { get; }
    IMongoCollection<Hotel> HotelsCollection { get; }
    IMongoCollection<Review> ReviewsCollection { get; }
    IMongoCollection<Reservation> ReservationCollection { get; }
    IMongoCollection<RoomType> RoomTypeCollection { get; }
}

public class MongoContext : IMongoContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly DatabaseSettings _settings;
    // readonly --> kan alleen in deze class aangepast worden maar nergens anders (zie constructor voor toewijzing waarden)

    public IMongoClient Client
    {
        get
        {
            return _client;
        }
    }
    public IMongoDatabase Database => _database;

    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<Hotel> HotelsCollection
    {
        get
        {
            return _database.GetCollection<Hotel>(_settings.HotelsCollection);
        }
    }

    public IMongoCollection<Review> ReviewsCollection
    {
        get
        {
            return _database.GetCollection<Review>(_settings.ReviewsCollection);
        }
    }

    public IMongoCollection<Reservation> ReservationCollection{
        get{ 
            return _database.GetCollection<Reservation>(_settings.ReservationsCollection);
        }
    }

    public IMongoCollection<RoomType> RoomTypeCollection{
        get{
            return _database.GetCollection<RoomType>(_settings.RoomTypesCollection);
        }
    }
}
