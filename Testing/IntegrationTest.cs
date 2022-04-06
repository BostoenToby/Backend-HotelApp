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
    public async Task Add_Hotel_Created(){
        var application = Helper.CreateApi();
        var client = application.CreateClient();
        var newHotel = new Hotel(){
            Name = "Hilton",
            City = "Brussel",
            Address = "Brusselsteenweg 321",
            Province = "Brussel",
            Longitude = 1.234567M,
            Latitude = 1.234567M,
            PricePerNightMin = 50.56M,
            PricePerNightMax = 500.98M
        };
        var result = await client.PostAsJsonAsync("/hotel", newHotel);
        Assert.NotNull(result.Headers.Location);
        result.StatusCode.Should().Be(HttpStatusCode.Created);

    }
}