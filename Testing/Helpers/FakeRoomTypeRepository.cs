namespace Testing.Repositories;

public class FakeRoomTypeRepository : IRoomTypeRepository
{
    public static List<RoomType> _roomtypes = new();

    public Task<RoomType> AddRoomType(RoomType newRoomType)
    {
        _roomtypes.Add(newRoomType);
        return Task.FromResult(newRoomType);
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

    public static void AddFakeRoomType(RoomType fakeRoomType) => _roomtypes.Add(fakeRoomType);

}
