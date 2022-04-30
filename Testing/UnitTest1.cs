using Xunit;

namespace Testing;

public class UnitTest1
{
    [Fact]
    public async Task Add_Hotel_ArgumentException()
    {
        var hotelService = Helper.CreateHotelService();
        Assert.ThrowsAsync<ArgumentException>(async() => hotelService.AddHotel(null));
    }

    [Fact]
    public async Task Add_Reservation_ArgumentException(){
        var hotelService = Helper.CreateHotelService();
        Assert.ThrowsAsync<ArgumentException>(async() => hotelService.AddReservation(null));
    }

    [Fact]
    public async Task Add_Review_ArgumentException(){
        var hotelService = Helper.CreateHotelService();
        Assert.ThrowsAsync<ArgumentException>(async() => hotelService.AddReview(null));
    }

    [Fact]
    public async Task Add_RoomType_ArgumentException(){
        var hotelService = Helper.CreateHotelService();
        Assert.ThrowsAsync<ArgumentException>(async() => hotelService.AddRoomType(null));
    }
}