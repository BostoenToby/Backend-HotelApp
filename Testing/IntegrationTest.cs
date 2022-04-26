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
            PricePerNightMax = 500.98M
        };
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
            Name = "Someone2",
            FirstName = "Random",
            BirthDate = DateTime.Parse("2002-07-04T13:33:03.969Z"),
            EMail = "random.someone@gmail.com",
            DateOfReservation = DateTime.Parse("2023-08-05T14:34:04.979Z"),
            TotalPrice = 480.89
        };
        newReservation.Hotel = new Hotel()
        {
            Name = "Ibis Hotel2.0",
            City = "Brussel",
            Address = "Brusselstraat 123",
            Province = "Brussel",
            Description = "It's a nice hotel",
            StarRating = 4.5F,
            Longitude = 1.234567M,
            Latitude = 1.234567M,
            PricePerNightMin = 150.89M,
            PricePerNightMax = 500.47M,
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
            Author = "SomeoneRandom",
            StarRating = 4.7,
            ReviewDescription = "It was a nice stay at the hotel"
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
            View = true
        };
        var result = await client.PostAsJsonAsync("/roomtype", newRoomType);
        Assert.NotNull(result.Headers.Location);
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }

}