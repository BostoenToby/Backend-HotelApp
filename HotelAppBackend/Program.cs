var builder = WebApplication.CreateBuilder(args);

var mongoSettings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(mongoSettings);

var authSettings = builder.Configuration.GetSection("AuthenticationSettings");
builder.Services.Configure<AuthenticationSettings>(authSettings);

builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<HotelValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ReservationValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ReviewValidator>());
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RoomTypeValidator>());

builder.Services.AddTransient<IHotelRepository, HotelRepository>();
builder.Services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<IHotelService, HotelService>();
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
    .AddMutationType<Mutation>();

builder.Services.AddAuthorization(options => {

});

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options => {
    options.TokenValidationParameters = new(){
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["AuthenticationSettings:Issuer"],
        ValidAudience = builder.Configuration["AuthenticationSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["AuthenticationSettings:SecretForKey"]))
    };
});

var app = builder.Build();
app.MapGraphQL();
app.MapSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/setup", () => "Hello World!");

app.MapGet("/hotels",[Authorize] async (IHotelService hotelService) => await hotelService.GetAllHotels());
app.MapGet("/hotel/{Id}", async (IHotelService hotelService, string Id) => await hotelService.GetHotelById(Id));
app.MapGet("/hotels/{NamePiece}", async (IHotelService hotelService, string NamePiece) => await hotelService.GetHotelsByNamePiece(NamePiece));
app.MapGet("/hotels/Region/{Region}", async(IHotelService hotelService, string Region) => await hotelService.GetHotelsByRegion(Region));
app.MapGet("/hotels/Filter/StarRating={StarRating}&PricePerNightMin={PricePerNightMin}&PricePerNightMax={PricePerNightMax}", async(IHotelService hotelService, decimal PricePerNightMin, decimal PricePerNightMax, float StarRating) => await hotelService.GetHotelsByFilter(PricePerNightMin, PricePerNightMax, StarRating));
app.MapGet("/hotels/Region/Filter/Region={Region}&PricePerNightMin={PricePerNightMin}&PricePerNightMax={PricePerNightMax}&StarRating={StarRating}", async(IHotelService hotelService, string Region, decimal PricePerNightMin, decimal PricePerNightMax, float StarRating) => await hotelService.GetHotelsByRegionAndFilter(Region, PricePerNightMin, PricePerNightMax, StarRating));
app.MapGet("/hotel/roomtype/Id={Id}&NumberOfBeds={NumberOfBeds}&SquareMeters={SquareMeters}&PriceMax={PriceMax}&PriceMin={PriceMin}&Television={Television}&Breakfast={Breakfast}&Airco={Airco}&Wifi={Wifi}&View={View}", async(IHotelService hotelService, string Id, int NumberOfBeds, float SquareMeters, float PriceMax, float PriceMin, bool Television, bool Breakfast, bool Airco, bool Wifi, bool View) => await hotelService.GetHotelRoomTypesByFilterAndId(Id, NumberOfBeds, SquareMeters, PriceMax, PriceMin, Television, Breakfast, Airco, Wifi, View));
app.MapGet("/roomtypes", async (IHotelService hotelService) => await hotelService.GetAllRoomTypes());
app.MapGet("/roomtype/{Id}", async (IHotelService hotelService, string Id) => await hotelService.GetRoomTypeById(Id));
app.MapGet("/roomtypes/roomname/{NamePiece}", async (IHotelService hotelService, string NamePiece) => await hotelService.GetRoomTypesByNamePiece(NamePiece));
app.MapGet("/roomtypes/hotelname/{HotelName}", async(IHotelService hotelService, string HotelName) => await hotelService.GetRoomTypesByHotelNamePiece(HotelName));
// TODO: Add the rest of the methods
app.MapGet("/reservations", async (IHotelService hotelService) => await hotelService.GetAllReservations());
app.MapGet("/reservations/Name={Name}&FirstName={FirstName}", async (IHotelService hotelService, string Name, string FirstName) => await hotelService.GetReservationsByName(Name, FirstName));
app.MapGet("/reservation/{Id}", async (IHotelService hotelService, string Id) => await hotelService.GetReservationById(Id));
app.MapGet("/reviews", async (IHotelService hotelService) => await hotelService.GetAllReviews());
app.MapGet("/reviews/{Author}", async (IHotelService hotelService, string Author) => await hotelService.GetReviewsByAuthor(Author));
app.MapGet("/review/{Id}", async (IHotelService hotelService, string Id) => await hotelService.GetReviewById(Id));
app.MapGet("/reviews/hotel/{HotelName}", async(IHotelService hotelService, string hotelName) => await hotelService.GetReviewsByHotel(hotelName));

app.MapPost("/authenticate", async (IAuthenticationService authenticationService, IOptions<AuthenticationSettings> authSettings, AuthenticationRequestBody authenticationRequestBody) =>
{
    var user = authenticationService.ValidateUser(authenticationRequestBody.username, authenticationRequestBody.password);
    if(user == null)
        return Results.Unauthorized();

    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(authSettings.Value.SecretForKey));

    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claimsForToken = new List<Claim>();
    claimsForToken.Add(new Claim("sub", "1"));
    claimsForToken.Add(new Claim("given_name", user.name));
    claimsForToken.Add(new Claim("city", user.city));

    var jwtSecurityToken = new JwtSecurityToken(
        authSettings.Value.Issuer,
        authSettings.Value.Audience,
        claimsForToken,
        DateTime.UtcNow,
        DateTime.UtcNow.AddHours(1000),
        signingCredentials
    );
    var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

    return Results.Ok(tokenToReturn);
});

app.MapPost("/hotel", async (IHotelService hotelService, IValidator<Hotel> validator, Hotel hotel) =>
{
    var validatorResult = validator.Validate(hotel);
    if (validatorResult.IsValid)
    {
        await hotelService.AddHotel(hotel);
        return Results.Created($"The hotel {hotel.Name} has been added to the database", hotel);
    }
    else
    {
        var errors = validatorResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});
app.MapPost("/roomtype", async (IHotelService hotelService, IValidator<RoomType> validator, RoomType roomType) =>
{
    var validatorResult = validator.Validate(roomType);
    if (validatorResult.IsValid)
    {
        await hotelService.AddRoomType(roomType);
        return Results.Created($"The roomtype {roomType.Name} has been added to the database", roomType);
    }
    else
    {
        var errors = validatorResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});
app.MapPost("/reservation", async (IHotelService hotelService, IValidator<Reservation> validator, Reservation reservation) =>
{
    var validatorResult = validator.Validate(reservation);
    if (validatorResult.IsValid)
    {
        await hotelService.AddReservation(reservation);
        return Results.Created($"The reservation from {reservation.Name} has been added to the database", reservation);
    }
    else
    {
        var errors = validatorResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});

app.MapPost("/review", async (IHotelService hotelService, IValidator<Review> validator, Review review) =>
{
    var validatorResult = validator.Validate(review);
    if (validatorResult.IsValid)
    {
        await hotelService.AddReview(review);
        return Results.Created($"The review from {review.Author} has been added to the database", review);
    }
    else
    {
        var errors = validatorResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});

app.MapPut("/hotel", async (IHotelService hotelService, IValidator<Hotel> validator, Hotel hotel) =>
{
    var validatorResult = validator.Validate(hotel);
    if (validatorResult.IsValid)
    {
        await hotelService.UpdateHotel(hotel);
        return Results.Created($"The hotel {hotel.Name} has been updated to the database", hotel);
    }
    else
    {
        var errors = validatorResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});
app.MapPut("/roomtype", async (IHotelService hotelService, IValidator<RoomType> validator, RoomType roomType) =>
{
    var validatorResult = validator.Validate(roomType);
    if (validatorResult.IsValid)
    {
        await hotelService.UpdateRoomType(roomType);
        return Results.Created($"The roomtype {roomType.Name} has been updated to the database", roomType);
    }
    else
    {
        var errors = validatorResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});
app.MapPut("/reservation", async (IHotelService hotelService, IValidator<Reservation> validator, Reservation reservation) =>
{
    var validatorResult = validator.Validate(reservation);
    if (validatorResult.IsValid)
    {
        await hotelService.UpdateReservation(reservation);
        return Results.Created($"The reservation from {reservation.Name} has been updated to the database", reservation);
    }
    else
    {
        var errors = validatorResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});
app.MapPut("/review", async (IHotelService hotelService, IValidator<Review> validator, Review review) =>
{
    var validatorResult = validator.Validate(review);
    if (validatorResult.IsValid)
    {
        await hotelService.UpdateReview(review);
        return Results.Created($"The review from {review.Author} has been updated to the database", review);
    }
    else
    {
        var errors = validatorResult.Errors.Select(x => new { errors = x.ErrorMessage });
        return Results.BadRequest(errors);
    }
});

app.MapDelete("/hotel/{Id}", async (IHotelService hotelService, string Id) => await hotelService.DeleteHotel(Id));
app.MapDelete("/roomtype/{Id}", async (IHotelService hotelService, string Id) => await hotelService.DeleteRoomType(Id));
app.MapDelete("/reservation/{Id}", async (IHotelService hotelService, string Id) => await hotelService.DeleteReservation(Id));
app.MapDelete("/review/{Id}", async (IHotelService hotelService, string Id) => await hotelService.DeleteReview(Id));

app.Run("http://0.0.0.0:3000");
// app.Run("http://localhost:3000");
// app.Run();
// public partial class Program { }
