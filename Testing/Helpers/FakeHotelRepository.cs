namespace Testing.Repositories;

public class FakeHotelRepository : IHotelRepository
{
    public static List<Hotel> _hotels = new();

    public Task<Hotel> AddHotel(Hotel newHotel)
    {
        _hotels.Add(newHotel);
        return Task.FromResult(newHotel);
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

    public static void AddFakeHotel(Hotel fakeHotel) => _hotels.Add(fakeHotel);

}