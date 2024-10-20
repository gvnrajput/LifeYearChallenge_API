using LYC_API.Service;

var builder = WebApplication.CreateBuilder(args);
//----File Path
var UjsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "ChallengeData.json");
builder.Services.AddSingleton(sp => UserService.GetInstance(UjsonFilePath));

var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "UserData.json");
builder.Services.AddSingleton(sp => ChallengeService.GetInstance(jsonFilePath));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
