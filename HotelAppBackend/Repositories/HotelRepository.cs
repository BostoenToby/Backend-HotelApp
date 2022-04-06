namespace Hotels.Repositories;

public interface IHotelRepository
{
    Task<Hotel> AddHotel(Hotel newHotel);
    Task<string> DeleteHotel(string Id);
    Task<List<Hotel>> GetAllHotels();
    Task<Hotel> GetHotelById(string Id);
    Task<List<Hotel>> GetHotelsByNamePiece(string NamePiece);
    Task<Hotel> UpdateHotel(Hotel hotel);
}

public class HotelRepository : IHotelRepository
{
    private readonly IMongoContext _context;
    public HotelRepository(IMongoContext context)
    {
        _context = context;
    }

    //GET
    public async Task<List<Hotel>> GetAllHotels() => await _context.HotelsCollection.Find<Hotel>(_ => true).ToListAsync();
    public async Task<List<Hotel>> GetHotelsByNamePiece(string NamePiece)
    {
        List<Hotel> allHotels = await _context.HotelsCollection.Find<Hotel>(_ => true).ToListAsync();
        List<Hotel> approvedHotels = new List<Hotel>();
        foreach (Hotel hotel in allHotels)
        {
            if (hotel.Name.Contains(NamePiece))
            {
                approvedHotels.Add(hotel);
            }
        }
        return approvedHotels;
    }
    public async Task<Hotel> GetHotelById(string Id) => await _context.HotelsCollection.Find<Hotel>(c => c.Id == Id).FirstOrDefaultAsync();

    //POST
    public async Task<Hotel> AddHotel(Hotel newHotel)
    {
        await _context.HotelsCollection.InsertOneAsync(newHotel);
        return newHotel;
    }

    //PUT
    public async Task<Hotel> UpdateHotel(Hotel hotel)
    {
        try
        {
            var filter = Builders<Hotel>.Filter.Eq("Id", hotel.Id);
            var update = Builders<Hotel>.Update.Set("Name", hotel.Name).Set("City", hotel.City).Set("Address", hotel.Address).Set("Province", hotel.Province).Set("Description", hotel.Description).Set("StarRating", hotel.StarRating).Set("Images", hotel.Images).Set("Longitude", hotel.Longitude).Set("Latitude", hotel.Latitude).Set("PricePerNightMin", hotel.PricePerNightMin).Set("PricePerNightMax", hotel.PricePerNightMax);
            var result = await _context.HotelsCollection.UpdateOneAsync(filter, update);
            return await GetHotelById(hotel.Id);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //DELETE
    public async Task<string> DeleteHotel(string Id)
    {
        try
        {
            var filter = Builders<Hotel>.Filter.Eq("Id", Id);
            var result = await _context.HotelsCollection.DeleteOneAsync(filter);
            return "The hotel has been removed succesfully";
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            return "The hotel hasn't been removed";
        }
    }
}