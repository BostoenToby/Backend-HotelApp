namespace Testing.Helpers;

public class Helper
{
    public static IHotelService CreateHotelService()
    {
        return CreateApi().Services.GetService<IHotelService>();
    }

    public static WebApplicationFactory<Program> CreateApi()
    {
        var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IHotelRepository));
                services.Remove(descriptor);

                var fakeHotelRepository = new ServiceDescriptor(typeof(IHotelRepository), typeof(FakeHotelRepository), ServiceLifetime.Transient);
                services.Add(fakeHotelRepository);


                // var descriptor2 = services.SingleOrDefault(c => c.ServiceType == typeof(IReservationRepository));
                // services.Remove(descriptor2);

                // var fakeReservationRepository = new ServiceDescriptor(typeof(IReservationRepository), typeof(FakeReservationRepository), ServiceLifetime.Transient);
                // services.Add(fakeReservationRepository);

                // var descriptor3 = services.SingleOrDefault(c => c.ServiceType == typeof(IReviewRepository));
                // services.Remove(descriptor3);

                // var fakeReviewRepository = new ServiceDescriptor(typeof(IReviewRepository), typeof(FakeReviewRepository), ServiceLifetime.Transient);
                // services.Add(fakeReviewRepository);

                // var descriptor4 = services.SingleOrDefault(c => c.ServiceType == typeof(IRoomTypeRepository));
                // services.Remove(descriptor4);

                // var fakeRoomTypeRepository = new ServiceDescriptor(typeof(IRoomTypeRepository), typeof(FakeReviewRepository), ServiceLifetime.Transient);
                // services.Add(fakeRoomTypeRepository);
            });
        });

        return application;

    }
}