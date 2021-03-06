namespace Hotels.GraphQL.Mutations;

public class Mutation
{
    // addHotel{addHotel(input: { Name:"", ...})}
    public async Task<AddHotelPayload> AddHotel([Service] IHotelService hotelService, AddHotelInput input)
    {
        var newHotel = new Hotel()
        {
            Name = input.Name,
            City = input.City,
            Address = input.Address,
            Province = input.Province,
            Longitude = input.Longitude,
            Latitude = input.Latitude,
            PricePerNightMin = input.PricePerNightMin,
            PricePerNightMax = input.PricePerNightMax,
            Description = input.Description,
            StarRating = input.StarRating,
            Image = input.Image,
            Reviews = input.Reviews,
            RoomTypes = input.RoomTypes
        };
        var created = await hotelService.AddHotel(newHotel);
        return new AddHotelPayload(created);
    }
    public async Task<AddReservationPayload> AddReservation([Service] IHotelService hotelService, AddReservationInput input)
    {
        var newReservation = new Reservation()
        {
            LastName = input.LastName,
            FirstName = input.FirstName,
            Mail = input.Mail,
            HotelName = input.HotelName,
            IncheckDate = input.IncheckDate,
            OutcheckDate = input.OutcheckDate,
            Price = input.Price,
            RoomTypeName = input.RoomTypeName
        };
        var created = await hotelService.AddReservation(newReservation);
        return new AddReservationPayload(created);
    }
    public async Task<AddReviewPayload> AddReview([Service] IHotelService hotelService, AddReviewInput input)
    {
        var newReview = new Review()
        {
            Author = input.Author,
            StarRating = input.StarRating,
            ReviewDescription = input.ReviewDescription,
            Image = input.Image,
            HotelName = input.HotelName
        };
        var created = await hotelService.AddReview(newReview);
        return new AddReviewPayload(created);
    }
    public async Task<AddRoomTypePayload> AddRoomType([Service] IHotelService hotelService, AddRoomTypeInput input){
        var newRoomType = new RoomType()
        {
            Name = input.Name,
            NumberOfBeds = input.NumberOfBeds,
            SquareMeters = input.SquareMeters,
            Television = input.Television,
            Breakfast = input.Breakfast,
            Airco = input.Airco,
            Wifi = input.Wifi,
            View = input.View,
            Price = input.Price,
            HotelName = input.HotelName,
            Image = input.Image
        };
        var created = await hotelService.AddRoomType(newRoomType);
        return new AddRoomTypePayload(created);
    }

    public async Task<AddHotelPayload> UpdateHotel([Service] IHotelService hotelService, UpdateHotelInput input){
        var updateHotel = new Hotel(){
            Id = input.Id,
            Name = input.Name,
            City = input.City,
            Address = input.Address,
            Province = input.Province,
            Longitude = input.Longitude,
            Latitude = input.Latitude,
            PricePerNightMin = input.PricePerNightMin,
            PricePerNightMax = input.PricePerNightMax,
            Description = input.Description,
            StarRating = input.StarRating,
            Reviews = input.Reviews,
            Image = input.Image,
            RoomTypes = input.RoomTypes
        };
        var updated = await hotelService.UpdateHotel(updateHotel);
        return new AddHotelPayload(updated);
    }
    public async Task<AddReservationPayload> UpdateReservation([Service] IHotelService hotelService, UpdateReservationInput input){
        var updateReservation = new Reservation(){
            Id = input.Id,
            LastName = input.LastName,
            FirstName = input.FirstName,
            Mail = input.Mail,
            HotelName = input.HotelName,
            IncheckDate = input.IncheckDate,
            OutcheckDate = input.OutcheckDate,
            Price = input.Price,
            RoomTypeName = input.RoomTypeName
        };
        var updated = await hotelService.UpdateReservation(updateReservation);
        return new AddReservationPayload(updated);
    }
    public async Task<AddReviewPayload> UpdateReview([Service] IHotelService hotelService, UpdateReviewInput input){
        var updateReview = new Review(){
            Id = input.Id,
            Author = input.Author,
            HotelName = input.HotelName,
            StarRating = input.StarRating,
            ReviewDescription = input.ReviewDescription,
            Image = input.Image
        };
        var updated = await hotelService.UpdateReview(updateReview);
        return new AddReviewPayload(updated);
    }
    public async Task<AddRoomTypePayload> UpdateRoomType([Service] IHotelService hotelService, UpdateRoomTypeInput input){
        var updateRoomType = new RoomType(){
            Id = input.Id,
            Name = input.Name,
            NumberOfBeds = input.NumberOfBeds,
            SquareMeters = input.SquareMeters,
            Television = input.Television,
            Breakfast = input.Breakfast,
            Airco = input.Airco,
            Wifi = input.Wifi,
            View = input.View,
            Price = input.Price,
            HotelName = input.HotelName,
            Image = input.Image
        };
        var updated = await hotelService.UpdateRoomType(updateRoomType);
        return new AddRoomTypePayload(updated);
    }
    
    public async Task<string> DeleteHotel([Service] IHotelService hotelService, string id){
        return await hotelService.DeleteHotel(id);
    }
    public async Task<string> DeleteReservation([Service] IHotelService hotelService, string id){
        return await hotelService.DeleteReservation(id);
    }
    public async Task<string> DeleteReview([Service] IHotelService hotelService, string id){
        return await hotelService.DeleteReview(id);
    }
    public async Task<string> DeleteRoomType([Service] IHotelService hotelService, string id){
        return await hotelService.DeleteRoomType(id);
    }
}