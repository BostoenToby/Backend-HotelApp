namespace Testing;

public class IntegrationTests
{
    [Fact]
    public async Task Should_Return_Hotels()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var result = await client.GetAsync("/hotels");
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var hotels = await result.Content.ReadFromJsonAsync<List<Hotel>>();
        Assert.NotNull(hotels);
        Assert.IsType<List<Hotel>>(hotels);
    }

    [Fact]
    public async Task Should_Return_Reservations()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var result = await client.GetAsync("/reservations");
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var reservations = await result.Content.ReadFromJsonAsync<List<Reservation>>();
        Assert.NotNull(reservations);
        Assert.IsType<List<Reservation>>(reservations);
    }

    [Fact]
    public async Task Should_Return_Reviews()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var result = await client.GetAsync("/reviews");
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var reviews = await result.Content.ReadFromJsonAsync<List<Review>>();
        Assert.NotNull(reviews);
        Assert.IsType<List<Review>>(reviews);
    }

    [Fact]
    public async Task Should_Return_RoomTypes()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var result = await client.GetAsync("/roomtypes");
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var roomTypes = await result.Content.ReadFromJsonAsync<List<RoomType>>();
        Assert.NotNull(roomTypes);
        Assert.IsType<List<RoomType>>(roomTypes);
    }

    [Fact]
    public async Task Add_Hotel_Created()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var newHotel = new Hotel()
        {
            Name = "Hilton",
            City = "Brussel",
            Address = "Brusselsteenweg 321",
            Province = "Brussel",
            StarRating = 3.5F,
            Longitude = 1.234567M,
            Latitude = 1.234567M,
            PricePerNightMin = 50.56M,
            PricePerNightMax = 500.98M,
            Image = "https://cf.bstatic.com/xdata/images/hotel/square600/335587995.webp?k=e52822ba4a04d4b4bf57c425cfca110c4e162fdcf4351efbd3b9f07c3b234a5e&o=&s=1",
            RoomTypes = new List<RoomType>()
        };
        var roomType = new RoomType(){
            Name =  "Presidential",
            NumberOfBeds = 2,
            SquareMeters = 50,
            Television = true,
            Breakfast = true,
            Airco = true,
            Wifi = true,
            View = true,
            Price = 210,
            HotelName = "Neptunus",
            Image = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/350348570.jpg?k=b09a56f8c5af3a67ada5f08fb7ef7a4b22a0b8cb8d01b32ba0cb77b00bf5e256&o="
        };
        newHotel.RoomTypes.Add(roomType);
        var result = await client.PostAsJsonAsync("/hotel", newHotel);
        Assert.NotNull(result.Headers.Location);
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Add_Reservation_Created()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var newReservation = new Reservation()
        {
            LastName = "Post",
            FirstName = "Pieter",
            Mail = "pieter.post@gmail.com",
            HotelName = "Ibis hotel",
            IncheckDate = "2022-05-02",
            OutcheckDate = "2022-05-04",
            Price = 134.56,
            RoomTypeName = "Presidential"
        };
        var result = await client.PostAsJsonAsync("/reservation", newReservation);
        Console.WriteLine(result);
        Assert.NotNull(result.Headers.Location);
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Add_Review_Created()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var newReview = new Review()
        {
            Author =  "Stewart",
            HotelName = "Ter Brughe",
            StarRating = 4.5,
            Image =  "https://cf.bstatic.com/static/img/review/avatars/ava-s/d321d61d78a8fa310843e1967dca38e6276b92aa.png",
            ReviewDescription = "They where friendly and the breakfast was nice. Good location."
        };
        var result = await client.PostAsJsonAsync("/review", newReview);
        Assert.NotNull(result.Headers.Location);
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Add_RoomType_Created()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var newRoomType = new RoomType()
        {
            Name = "Presidential",
            NumberOfBeds = 5,
            SquareMeters = 45.65F,
            Television = true,
            Breakfast = true,
            Airco = true,
            Wifi = true,
            View = true,
            Price = 99,
            HotelName = "Neptunus",
            Image = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/350348631.jpg?k=2c3236726e2f72dd1aa2b25395bd3c4f650c7f467c1ecb8e80852f867f32862e&o="
        };
        var result = await client.PostAsJsonAsync("/roomtype", newRoomType);
        Assert.NotNull(result.Headers.Location);
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Should_Return_BadRequest_Hotel()
    {
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var newHotel = new Hotel()
        {
            Name = "Hi",
            City = "Brussel",
            Address = "Brusselsteenweg 321",
            Province = "Brussel",
            StarRating = 3.5F,
            Longitude = 1.234567M,
            Latitude = 1.234567M,
            PricePerNightMin = 50.56M,
            PricePerNightMax = 500.98M,
            Image = "https://cf.bstatic.com/xdata/images/hotel/square600/335587995.webp?k=e52822ba4a04d4b4bf57c425cfca110c4e162fdcf4351efbd3b9f07c3b234a5e&o=&s=1",
            RoomTypes = new List<RoomType>()
        };
        var roomType = new RoomType(){
            Name =  "Presidential",
            NumberOfBeds = 2,
            SquareMeters = 50,
            Television = true,
            Breakfast = true,
            Airco = true,
            Wifi = true,
            View = true,
            Price = 210,
            HotelName = "Neptunus",
            Image = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/350348570.jpg?k=b09a56f8c5af3a67ada5f08fb7ef7a4b22a0b8cb8d01b32ba0cb77b00bf5e256&o="
        };
        newHotel.RoomTypes.Add(roomType);
        var result = await client.PostAsJsonAsync("/hotel", newHotel);
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

}