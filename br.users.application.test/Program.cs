using br.users.application.test.Api.Extensions;
using br.users.application.test.crossCutting.IoC;
using br.users.application.test.domain.Entities;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));

var appSetting = new AppSettings();
builder.Configuration.GetSection(nameof(AppSettings)).Bind(appSetting);
builder.Services.AddSingleton(appSetting);

builder.Services.RegisterAllClasses(builder.Configuration);


//ServiceExtensions.RegisterSwagger(builder.Services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
