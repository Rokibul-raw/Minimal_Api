using Microsoft.EntityFrameworkCore;
using MinimalApi.Data;
using MinimalApi.Models;

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
//Post Data
app.MapPost("/Post_Data",async (Visitor_Info visitor_Info ,VistorDbContext context) =>
{
    context.VisitorInfos.Add(visitor_Info);
    await context.SaveChangesAsync();
    return Results.Ok();

}).WithName("Post_User");
app.MapPut("/Update_Data", async(Visitor_Info visitor_Info, VistorDbContext context) =>
{
    var update_data = await context.VisitorInfos.FindAsync(visitor_Info.Id);
    if(update_data != null)
    {
        update_data.FullName = visitor_Info.FullName;
        update_data.Mob_No=visitor_Info.Mob_No;
        update_data.Address = visitor_Info.Address;
        await context.SaveChangesAsync();
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
}).WithName("Update Data");

app.MapDelete("/Delete_Data", async (int id, VistorDbContext context) =>
{
    var delete_data=await context.VisitorInfos.FindAsync(id);
    if(delete_data != null)
    {
        context.VisitorInfos.Remove(delete_data);
        await context.SaveChangesAsync();
        return Results.Ok();
    }
    else
    {
        return Results.NoContent();
    }

}).WithName("Delete Data");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}