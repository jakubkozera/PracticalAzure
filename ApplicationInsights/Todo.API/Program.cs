var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", builder =>
    {
        builder.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(new string[] { "http://localhost:3000", "https://sample1-ui.azurewebsites.net" })
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FrontEndClient");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();