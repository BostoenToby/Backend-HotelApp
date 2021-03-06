namespace Hotels.Repositories;

public interface IHotelRepository
{
    Task<Hotel> AddHotel(Hotel newHotel);
    Task<string> DeleteHotel(string Id);
    Task<List<Hotel>> GetAllHotels();
    Task<Hotel> GetHotelById(string Id);
    Task<List<RoomType>> GetHotelRoomTypesByFilterAndId(string Id, int NumberOfBeds, float SquareMeters, float PriceMax, float PriceMin, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View);
    Task<List<Hotel>> GetHotelsByFilter(decimal PricePerNightMin, decimal PricePerNightMax, float StarRating);
    Task<List<Hotel>> GetHotelsByFilterAndNamepiece(decimal PricePerNightMin, decimal PricePerNightMax, float StarRating, string Namepiece);
    Task<List<Hotel>> GetHotelsByNamePiece(string NamePiece);
    Task<List<Hotel>> GetHotelsByRegion(string Region);
    Task<List<Hotel>> GetHotelsByRegionAndFilter(string Region, decimal PricePerNightMin, decimal PricePerNightMax, float StarRating);
    Task<List<Hotel>> GetHotelsByRegionAndFilterAndNamepiece(string Region, decimal PricePerNightMin, decimal PricePerNightMax, float StarRating, string Namepiece);
    Task<Hotel> UpdateHotel(Hotel hotel);
}

public class HotelRepository : IHotelRepository
{
    private readonly IMongoContext _context;
    public HotelRepository(IMongoContext context)
    {
        _context = context;
    }

    List<string> Provinces = new List<string>(){
        "West-Vlaanderen",
        "Oost-Vlaanderen",
        "Antwerpen",
        "Henegouwen",
        "Limburg",
        "Luik",
        "Luxemburg",
        "Namen",
        "Vlaams-Brabant",
        "Waals-Brabant"
    };

    //GET
    public async Task<List<Hotel>> GetAllHotels() => await _context.HotelsCollection.Find<Hotel>(_ => true).ToListAsync();
    public async Task<List<Hotel>> GetHotelsByNamePiece(string NamePiece)
    {
        List<Hotel> allHotels = await _context.HotelsCollection.Find<Hotel>(_ => true).ToListAsync();
        List<Hotel> approvedHotels = new List<Hotel>();
        foreach (Hotel hotel in allHotels)
            if (hotel.Name.Contains(NamePiece))
                approvedHotels.Add(hotel);
        return approvedHotels;
    }

    public async Task<List<Hotel>> GetHotelsByFilter(decimal PricePerNightMin, decimal PricePerNightMax, float StarRating)
    {
        // filter is necessary
        // if decimal is not filled in --> PricePerNightMin = 0 & PricePerNightMax = 1000000
        // if float is not filled in --> -1 
        // these values will be given in the frontend 
        // this will include all the hotels
        List<Hotel> allHotels = await GetAllHotels();
        List<Hotel> chosenHotels = new List<Hotel>();
        foreach (Hotel hotel in allHotels)
            if (PricePerNightMin <= hotel.PricePerNightMin)
                if (PricePerNightMax >= hotel.PricePerNightMax)
                    if (StarRating <= hotel.StarRating)
                        chosenHotels.Add(hotel);
        return chosenHotels;
    }

    public async Task<List<Hotel>> GetHotelsByRegion(string Region)
    {
        List<Hotel> chosenHotels = new List<Hotel>();
        if (Provinces.Contains(Region))
            chosenHotels = await _context.HotelsCollection.Find(c => c.Province.Contains(Region)).ToListAsync();
        else
            chosenHotels = await _context.HotelsCollection.Find(c => c.City.Contains(Region)).ToListAsync();
        return chosenHotels;
    }

    public async Task<List<Hotel>> GetHotelsByRegionAndFilter(string Region, decimal PricePerNightMin, decimal PricePerNightMax, float StarRating)
    {
        List<Hotel> chosenHotelsRegion = new List<Hotel>();
        List<Hotel> chosenHotels = new List<Hotel>();
        if (Provinces.Contains(Region))
            chosenHotelsRegion = await _context.HotelsCollection.Find(c => c.Province.Contains(Region)).ToListAsync();
        else
            chosenHotelsRegion = await _context.HotelsCollection.Find(c => c.City.Contains(Region)).ToListAsync();
        foreach (Hotel hotel in chosenHotelsRegion)
            if (PricePerNightMin <= hotel.PricePerNightMin)
                if (PricePerNightMax >= hotel.PricePerNightMax)
                    if (StarRating <= hotel.StarRating)
                        chosenHotels.Add(hotel);
        return chosenHotels;
    }

    public async Task<List<Hotel>> GetHotelsByFilterAndNamepiece(decimal PricePerNightMin, decimal PricePerNightMax, float StarRating, string Namepiece)
    {
        List<Hotel> hotels = new List<Hotel>();
        List<Hotel> chosenHotels = new List<Hotel>();
        hotels = await GetAllHotels();
        foreach (Hotel hotel in hotels)
            if (PricePerNightMin <= hotel.PricePerNightMin)
                if (PricePerNightMax >= hotel.PricePerNightMax)
                    if (StarRating <= hotel.StarRating)
                        chosenHotels.Add(hotel);

        foreach (Hotel hotel in chosenHotels.ToList())
            if (!hotel.Name.Contains(Namepiece))
                chosenHotels.Remove(hotel);

        return chosenHotels;
    }

    public async Task<List<Hotel>> GetHotelsByRegionAndFilterAndNamepiece(string Region, decimal PricePerNightMin, decimal PricePerNightMax, float StarRating, string Namepiece)
    {
        List<Hotel> chosenHotelsRegion = new List<Hotel>();
        List<Hotel> chosenHotels = new List<Hotel>();
        if (Provinces.Contains(Region))
            chosenHotelsRegion = await _context.HotelsCollection.Find(c => c.Province.Contains(Region)).ToListAsync();
        else
            chosenHotelsRegion = await _context.HotelsCollection.Find(c => c.City.Contains(Region)).ToListAsync();
        foreach (Hotel hotel in chosenHotelsRegion)
            if (PricePerNightMin <= hotel.PricePerNightMin)
                if (PricePerNightMax >= hotel.PricePerNightMax)
                    if (StarRating <= hotel.StarRating)
                        chosenHotels.Add(hotel);

        foreach (Hotel hotel in chosenHotels)
            if (!hotel.Name.Contains(Namepiece))
                chosenHotels.Remove(hotel);

        return chosenHotels;
    }
    // TODO: hotels by region and filter and namepiece

    public async Task<List<RoomType>> GetHotelRoomTypesByFilterAndId(string Id, int NumberOfBeds, float SquareMeters, float PriceMax, float PriceMin, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View)
    {
        Hotel chosenHotel = await GetHotelById(Id);
        List<RoomType> chosenRoomTypes = new List<RoomType>();
        foreach (RoomType roomType in chosenHotel.RoomTypes.ToList())
        {
            if (roomType.NumberOfBeds >= NumberOfBeds && roomType.SquareMeters >= SquareMeters && roomType.Price <= PriceMax && roomType.Price >= PriceMin)
                chosenRoomTypes.Add(roomType);
        }
        foreach (RoomType roomType in chosenRoomTypes.ToList())
        {
            if (Television == true && roomType.Television != true)
            {
                chosenRoomTypes.Remove(roomType);
            }
            if (Breakfast == true && roomType.Breakfast != true)
            {
                chosenRoomTypes.Remove(roomType);
            }
            if (Airco == true && roomType.Breakfast != true)
            {
                chosenRoomTypes.Remove(roomType);
            }
            if (Wifi == true && roomType.Breakfast != true)
            {
                chosenRoomTypes.Remove(roomType);
            }
            if (View == true && roomType.View != true)
            {
                chosenRoomTypes.Remove(roomType);
            }
        }
        return chosenRoomTypes;
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
            var update = Builders<Hotel>.Update.Set("Name", hotel.Name).Set("City", hotel.City).Set("Address", hotel.Address).Set("Province", hotel.Province).Set("Description", hotel.Description).Set("StarRating", hotel.StarRating).Set("Image", hotel.Image).Set("Longitude", hotel.Longitude).Set("Latitude", hotel.Latitude).Set("PricePerNightMin", hotel.PricePerNightMin).Set("PricePerNightMax", hotel.PricePerNightMax).Set("Reviews", hotel.Reviews).Set("RoomTypes", hotel.RoomTypes);
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