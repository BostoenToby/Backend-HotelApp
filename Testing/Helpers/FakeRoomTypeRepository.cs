namespace Testing.Repositories;

public class FakeRoomTypeRepository : IRoomTypeRepository
{
    public static List<RoomType> _roomtypes = new();

    public Task<RoomType> AddRoomType(RoomType newRoomType)
    {
        _roomtypes.Add(newRoomType);
        return Task.FromResult(newRoomType);
    }

    public Task<List<RoomType>> GetAllRoomTypes()
    {
        return Task.FromResult(_roomtypes);
    }

    public Task<RoomType> GetRoomTypeById(string Id)
    {
        RoomType roomTypeFound = _roomtypes.Find(c => c.Id == Id);
        return Task.FromResult(roomTypeFound);
    }

    public Task<List<RoomType>> GetRoomTypesByNamePiece(string NamePiece)
    {
        List<RoomType> roomTypesFound = new List<RoomType>();
        foreach (RoomType roomType in _roomtypes)
        {
            if (roomType.Name.Contains(NamePiece))
            {
                roomTypesFound.Add(roomType);
            }
        }
        return Task.FromResult(roomTypesFound);
    }

    public Task<List<RoomType>> GetRoomTypesByFilter(int NumberOfBeds, float SquareMeters, float PriceMax, float PriceMin, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View)
    {
        List<RoomType> roomTypesFound = new List<RoomType>();
        foreach (RoomType roomType in _roomtypes)
            if (roomType.NumberOfBeds >= NumberOfBeds && roomType.SquareMeters >= SquareMeters && roomType.Price <= PriceMax && roomType.Price >= PriceMin)
                roomTypesFound.Add(roomType);

        foreach (RoomType roomType in roomTypesFound)
        {
            if (Television == true && roomType.Television != true)
            {
                roomTypesFound.Remove(roomType);
            }
            if (Breakfast == true && roomType.Breakfast != true)
            {
                roomTypesFound.Remove(roomType);
            }
            if (Airco == true && roomType.Breakfast != true)
            {
                roomTypesFound.Remove(roomType);
            }
            if (Wifi == true && roomType.Breakfast != true)
            {
                roomTypesFound.Remove(roomType);
            }
            if (View == true && roomType.View != true)
            {
                roomTypesFound.Remove(roomType);
            }
        }
        return Task.FromResult(roomTypesFound);
    }

    public Task<List<RoomType>> GetRoomTypesByHotelNamePiece(string HotelName)
    {
        List<RoomType> roomTypesFound = new List<RoomType>();
        foreach (RoomType roomType in _roomtypes)
            if (roomType.HotelName.Contains(HotelName))
                roomTypesFound.Add(roomType);

        return Task.FromResult(roomTypesFound);
    }

    public Task<List<RoomType>> GetRoomTypesByNamePieceAndFilter(string NamePiece, int NumberOfBeds, float SquareMeters, float PriceMax, float PriceMin, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View)
    {
        List<RoomType> roomTypesFoundName = new List<RoomType>();
        List<RoomType> roomTypesFound = new List<RoomType>();
        foreach (RoomType roomType in _roomtypes)
            if (roomType.Name.Contains(NamePiece))
                roomTypesFoundName.Add(roomType);

        foreach (RoomType roomType in roomTypesFoundName)
            if (roomType.NumberOfBeds >= NumberOfBeds && roomType.SquareMeters >= SquareMeters && roomType.Price <= PriceMax && roomType.Price >= PriceMin)
                roomTypesFound.Add(roomType);

        foreach (RoomType roomType in roomTypesFound)
        {
            if (Television == true && roomType.Television != true)
            {
                roomTypesFound.Remove(roomType);
            }
            if (Breakfast == true && roomType.Breakfast != true)
            {
                roomTypesFound.Remove(roomType);
            }
            if (Airco == true && roomType.Breakfast != true)
            {
                roomTypesFound.Remove(roomType);
            }
            if (Wifi == true && roomType.Breakfast != true)
            {
                roomTypesFound.Remove(roomType);
            }
            if (View == true && roomType.View != true)
            {
                roomTypesFound.Remove(roomType);
            }
        }
        return Task.FromResult(roomTypesFound);
    }

    public Task<RoomType> UpdateRoomType(RoomType roomType)
    {
        try
        {
            RoomType roomTypeFound = _roomtypes.Where(c => c.Id == roomType.Id).SingleOrDefault();
            if (roomTypeFound != null)
            {
                _roomtypes.Remove(roomTypeFound);
                _roomtypes.Add(roomType);
            }
            return Task.FromResult(roomType);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public Task<string> DeleteRoomType(string Id)
    {
        try
        {
            RoomType roomTypeFound = _roomtypes.Find(c => c.Id == Id);
            _roomtypes.Remove(roomTypeFound);
            return Task.Run(() => "The roomType was removed successfully");
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            return Task.Run(() => "The roomType wasn't removed successfully");
        }
    }

    public static void AddFakeRoomType(RoomType fakeRoomType) => _roomtypes.Add(fakeRoomType);
}
