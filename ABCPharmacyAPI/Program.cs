using ABCPharmacyAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS with multiple policies
//builder.Services.AddCors(options =>
//{
//    // Policy for specific origins (Production)
//    options.AddPolicy("Production", policy =>
//    {
//        policy.WithOrigins(
//                "https://yourdomain.com",
//                "https://www.yourdomain.com"
//              )
//              .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
//              .WithHeaders("Authorization", "Content-Type", "Accept")
//              .AllowCredentials();
//    });
//    // Policy for development with multiple local origins
//    options.AddPolicy("Development", policy =>
//    {
//        policy.WithOrigins(
//                "http://localhost:60783",
//                "https://localhost:3000",
//                "http://localhost:5000",
//                "https://localhost:4200",
//                "http://127.0.0.1:5500",
//                "http://localhost:8080"
//              )
//              .AllowAnyMethod()
//              .AllowAnyHeader()
//              .AllowCredentials();
//    });
//    // Policy for testing/quick development (allows any origin)
//    options.AddPolicy("AllowAll", policy =>
//    {
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader();
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddScoped<IMedicineService, MedicineService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Use development CORS policy
    app.UseCors("AllowAngularApp");
}
//else
//{
//    // Use production CORS policy
//    app.UseCors("Production");
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
