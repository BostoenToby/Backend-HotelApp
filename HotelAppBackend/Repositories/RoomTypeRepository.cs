namespace Hotels.Repositories;

public interface IRoomTypeRepository
{
    Task<RoomType> AddRoomType(RoomType newRoomType);
    Task<string> DeleteRoomType(string Id);
    Task<List<RoomType>> GetAllRoomTypes();
    Task<RoomType> GetRoomTypeById(string Id);
    Task<List<RoomType>> GetRoomTypesByFilter(int NumberOfBeds, float SquareMeters, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View);
    Task<List<RoomType>> GetRoomTypesByNamePiece(string NamePiece);
    Task<List<RoomType>> GetRoomTypesByNamePieceAndFilter(string NamePiece, int NumberOfBeds, float SquareMeters, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View);
    Task<RoomType> UpdateRoomType(RoomType roomType);
}

public class RoomTypeRepository : IRoomTypeRepository
{
    private readonly IMongoContext _context;
    public RoomTypeRepository(IMongoContext context)
    {
        _context = context;
    }

    //GET
    public async Task<List<RoomType>> GetAllRoomTypes() => await _context.RoomTypeCollection.Find<RoomType>(_ => true).ToListAsync();
    public async Task<List<RoomType>> GetRoomTypesByNamePiece(string NamePiece)
    {
        List<RoomType> AllRoomTypes = await GetAllRoomTypes();
        List<RoomType> ApprovedRoomTypes = new List<RoomType>();

        foreach (RoomType roomType in AllRoomTypes)
        {
            if (roomType.Name.Contains(NamePiece))
            {
                ApprovedRoomTypes.Add(roomType);
            }
        }
        return ApprovedRoomTypes;
    }
    public async Task<List<RoomType>> GetRoomTypesByFilter(int NumberOfBeds, float SquareMeters, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View)
    {
        List<RoomType> allRoomTypes = await GetAllRoomTypes();
        List<RoomType> chosenRoomTypes = new List<RoomType>();
        foreach (RoomType roomType in allRoomTypes) //scenario where all parameters are filled in
            if (roomType.NumberOfBeds >= NumberOfBeds && roomType.SquareMeters >= SquareMeters && roomType.Television == Television && roomType.Breakfast == Breakfast && roomType.Airco == Airco && roomType.Wifi == Wifi && roomType.View == View)
                chosenRoomTypes.Add(roomType);

        return chosenRoomTypes;
    }
    public async Task<List<RoomType>> GetRoomTypesByNamePieceAndFilter(string NamePiece, int NumberOfBeds, float SquareMeters, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View)
    {
        List<RoomType> AllRoomTypes = await GetAllRoomTypes();
        List<RoomType> RoomTypesName = new List<RoomType>();
        List<RoomType> chosenRoomTypes = new List<RoomType>();

        foreach (RoomType roomType in AllRoomTypes)
        {
            if (roomType.Name.Contains(NamePiece))
            {
                RoomTypesName.Add(roomType);
            }
        }
        foreach (RoomType roomType in RoomTypesName)
            if (roomType.NumberOfBeds == NumberOfBeds && roomType.SquareMeters >= SquareMeters && roomType.Television == Television && roomType.Breakfast == Breakfast && roomType.Airco == Airco && roomType.Wifi == Wifi && roomType.View == View)
                chosenRoomTypes.Add(roomType);
        return chosenRoomTypes;
    }
    public async Task<RoomType> GetRoomTypeById(string Id) => await _context.RoomTypeCollection.Find<RoomType>(c => c.Id == Id).FirstOrDefaultAsync();

    //POST
    public async Task<RoomType> AddRoomType(RoomType newRoomType)
    {
        await _context.RoomTypeCollection.InsertOneAsync(newRoomType);
        return newRoomType;
    }

    //PUT
    public async Task<RoomType> UpdateRoomType(RoomType roomType)
    {
        try
        {
            var filter = Builders<RoomType>.Filter.Eq("Id", roomType.Id);
            var update = Builders<RoomType>.Update.Set("Name", roomType.Name).Set("NumberOfBeds", roomType.NumberOfBeds).Set("SquareMeters", roomType.SquareMeters).Set("Television", roomType.Television).Set("Breakfast", roomType.Breakfast).Set("Airco", roomType.Airco).Set("Wifi", roomType.Wifi).Set("View", roomType.View);
            var result = await _context.RoomTypeCollection.UpdateOneAsync(filter, update);
            return await GetRoomTypeById(roomType.Id);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //DELETE
    public async Task<string> DeleteRoomType(string Id)
    {
        try
        {
            var filter = Builders<RoomType>.Filter.Eq("Id", Id);
            var result = await _context.RoomTypeCollection.DeleteOneAsync(filter);
            return "The roomtype has been removed succesfully";
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex);
            return "The roomtype hasn't been removed";
        }
    }
}