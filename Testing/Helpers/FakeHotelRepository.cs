namespace Testing.Repositories;

public class FakeHotelRepository : IHotelRepository
{
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
    public static List<Hotel> _hotels = new();

    public Task<Hotel> AddHotel(Hotel newHotel)
    {
        _hotels.Add(newHotel);
        return Task.FromResult(newHotel);
    }

    public Task<List<Hotel>> GetAllHotels()
    {
        return Task.FromResult(_hotels);
    }

    public Task<Hotel> GetHotelById(string Id)
    {
        Hotel hotelFound = _hotels.Find(c => c.Id == Id);
        return Task.FromResult(hotelFound);
    }

    public Task<List<Hotel>> GetHotelsByNamePiece(string NamePiece)
    {
        List<Hotel> hotelsFound = new List<Hotel>();
        foreach (Hotel hotel in _hotels)
        {
            if (hotel.Name.Contains(NamePiece))
            {
                hotelsFound.Add(hotel);
            }
        }
        return Task.FromResult(hotelsFound);
    }

    public Task<List<RoomType>> GetHotelRoomTypesByFilterAndId(string Id, int NumberOfBeds, float SquareMeters, float PriceMax, float PriceMin, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View)
    {
        List<RoomType> roomtypesFound = new List<RoomType>();
        Hotel hotelFound = _hotels.Find(c => c.Id == Id);
        foreach (RoomType roomType in hotelFound.RoomTypes)
            if (roomType.NumberOfBeds >= NumberOfBeds && roomType.SquareMeters >= SquareMeters && roomType.Price <= PriceMax && roomType.Price >= PriceMin)
                roomtypesFound.Add(roomType);

        foreach (RoomType roomType in roomtypesFound)
        {
            if (Television == true && roomType.Television != true)
            {
                roomtypesFound.Remove(roomType);
            }
            if (Breakfast == true && roomType.Breakfast != true)
            {
                roomtypesFound.Remove(roomType);
            }
            if (Airco == true && roomType.Breakfast != true)
            {
                roomtypesFound.Remove(roomType);
            }
            if (Wifi == true && roomType.Breakfast != true)
            {
                roomtypesFound.Remove(roomType);
            }
            if (View == true && roomType.View != true)
            {
                roomtypesFound.Remove(roomType);
            }
        }
        return Task.FromResult(roomtypesFound);
    }

    public Task<List<Hotel>> GetHotelsByFilter(decimal PricePerNightMin, decimal PricePerNightMax, float StarRating)
    {
        List<Hotel> hotelsFound = new List<Hotel>();
        foreach(Hotel hotel in _hotels)
        if(PricePerNightMin <= hotel.PricePerNightMin)
        if(PricePerNightMax >= hotel.PricePerNightMax)
        if(StarRating <= hotel.StarRating)
        hotelsFound.Add(hotel);

        return Task.FromResult(hotelsFound);
    }

    public Task<List<Hotel>> GetHotelsByRegion(string Region)
    {
        List<Hotel> hotelsFound = new List<Hotel>();
        if(Provinces.Contains(Region)){
            foreach(Hotel hotel in _hotels)
            if(hotel.Province == Region)
            hotelsFound.Add(hotel);
        }
        else{
            foreach(Hotel hotel in _hotels)
            if(hotel.City == Region)
            hotelsFound.Add(hotel);
        }
        return Task.FromResult(hotelsFound);   
    }

    public Task<List<Hotel>> GetHotelsByRegionAndFilter(string Region, decimal PricePerNightMin, decimal PricePerNightMax, float StarRating)
    {
        List<Hotel> hotelsFoundRegion = new List<Hotel>();
        List<Hotel> hotelsFound = new List<Hotel>();
        if(Provinces.Contains(Region)){
            foreach(Hotel hotel in _hotels)
            if(hotel.Province == Region)
            hotelsFoundRegion.Add(hotel);
        }
        else{
            foreach(Hotel hotel in _hotels)
            if(hotel.City == Region)
            hotelsFoundRegion.Add(hotel);
        }
        foreach(Hotel hotel in hotelsFoundRegion)
        if(PricePerNightMin <= hotel.PricePerNightMin)
        if(PricePerNightMax >= hotel.PricePerNightMax)
        if(StarRating <= hotel.StarRating)
        hotelsFound.Add(hotel);
        
        return Task.FromResult(hotelsFound);
    }

    public Task<Hotel> UpdateHotel(Hotel hotel)
    {
        try
        {
            Hotel hotelFound = _hotels.Where(c => c.Id == hotel.Id).SingleOrDefault();
            if (hotelFound != null)
            {
                _hotels.Remove(hotelFound);
                _hotels.Add(hotel);
            }
            return Task.FromResult(hotel);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<string> DeleteHotel(string Id)
    {
        try
        {
            Hotel hotelFound = _hotels.Find(c => c.Id == Id);
            _hotels.Remove(hotelFound);
            return Task.Run(() => "The hotel was removed successfully");
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            return Task.Run(() => "The hotel wasn't removed successfully");
        }
    }

    public static void AddFakeHotel(Hotel fakeHotel) => _hotels.Add(fakeHotel);

    public Task<List<Hotel>> GetHotelsByFilterAndNamepiece(decimal PricePerNightMin, decimal PricePerNightMax, float StarRating, string Namepiece)
    {
        List<Hotel> chosenHotels = new List<Hotel>();
        foreach(Hotel hotel in _hotels){
            if(PricePerNightMin <= hotel.PricePerNightMin)
                if(PricePerNightMax >= hotel.PricePerNightMax)
                    if(StarRating <= hotel.StarRating)
                        chosenHotels.Add(hotel);
        }
        foreach(Hotel hotel in chosenHotels){
            if(!hotel.Name.Contains(Namepiece))
                chosenHotels.Remove(hotel);
        }

        return Task.FromResult(chosenHotels);
    }

    public Task<List<Hotel>> GetHotelsByRegionAndFilterAndNamepiece(string Region, decimal PricePerNightMin, decimal PricePerNightMax, float StarRating, string Namepiece)
    {
        List<Hotel> chosenHotelsRegion = new List<Hotel>();
        List<Hotel> chosenHotels = new List<Hotel>();
        if(Provinces.Contains(Region)){
            foreach(Hotel hotel in _hotels){
                if(hotel.Province.Contains(Region)){
                    chosenHotelsRegion.Add(hotel);
                }
            }
        } else {
            foreach(Hotel hotel in _hotels){
                if(hotel.City.Contains(Region)){
                    chosenHotelsRegion.Add(hotel);
                }
            }
        }
        foreach(Hotel hotel in chosenHotelsRegion){
            if(PricePerNightMin <= hotel.PricePerNightMin)
                if(PricePerNightMax >= hotel.PricePerNightMax)
                    if(StarRating <= hotel.StarRating)
                        chosenHotels.Add(hotel);
        }
        foreach(Hotel hotel in chosenHotels){
            if(!hotel.Name.Contains(Namepiece))
                chosenHotels.Remove(hotel);
        }
        return Task.FromResult(chosenHotels);
    }
}