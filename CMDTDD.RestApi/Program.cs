using CMDTDD.Persistanse.EF;
using CMDTDD.Services.Complexes.Contracts;
using Microsoft.EntityFrameworkCore;
using CMDTDD.Services.Contracts;
using CMS.Service.Unit.Test.Blocks;
using CMS.Service.Unit.Test.Complexes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EFDataContext>(_ =>
    _.UseSqlServer("Server=.;Database=CMSTDDDATABASE;Trusted_Connection=True;"));
builder.Services.AddScoped<UnitOfWork, EfUnitOfWork>();
builder.Services.AddScoped<ComplexService,ComplexAppService>();
builder.Services.AddScoped<ComplexRepasitory,EFComplexRepasitory>();
builder.Services.AddScoped<BlockRepasitory,EFBlockRepasitory>();
builder.Services.AddScoped<BlockService,BlockAppService>();



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

app.Run();