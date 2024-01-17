using Microsoft.EntityFrameworkCore;
using VehicleManager.Data;
using VehicleManager.Repository.MaintenanceRepository;
using VehicleManager.Repository.VehicleRepository;
using VehicleManager.Services.MaintenanceService;
using VehicleManager.Services.VehicleService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
//builder.Services.AddScoped<IVehicleRepository, MockVehicleRepository>();
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
//builder.Services.AddScoped<IMaintenanceRepository, MockMaintenanceRepository>();

builder.Services.AddDbContext<VehicleManagerContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("VehicleManagerConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAngularDev");

app.Run();
