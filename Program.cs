using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<VistorDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

//Get user by single Id

app.MapGet("/Get_Data_By_Id",async(int id, VistorDbContext context) =>
{
    var get_by_id = await context.VisitorInfos.FindAsync(id);
    return Results.Ok(get_by_id);

}).WithName("GetBy_Single_Id");

//Get all data
app.MapGet("/Get_All_Data", async (VistorDbContext context) =>
{
    var get_all_data = await context.VisitorInfos.ToListAsync();
    return Results.Ok(get_all_data);

});
    

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}