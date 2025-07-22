using br.users.application.test.crossCutting.IoC;
using br.users.application.test.domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var baseCorsPolicy = "_basePolicy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "YourApp API", Version = "v1" });

    var jwtScheme = new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "Insira 'Bearer' [espaço] e o token JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtScheme.Reference.Id, jwtScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { jwtScheme, Array.Empty<string>() }
    });
});
//builder.Services.Configure<AppSettings>(builder.Configuration.GetSection(nameof(AppSettings)));
//builder.Services.AddCors(option => option.AddDefaultPolicy(policy =>
//{
//    policy.AllowAnyOrigin();
//    policy.AllowAnyMethod();
//    policy.AllowAnyHeader();
//}));
//- CORS
builder.Services.AddCors(opt => {
    opt.AddPolicy(name: baseCorsPolicy, policy => {
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
        policy.WithOrigins(["http://localhost:4200", "http://localhost:*", "https://localhost/*", "http://localhost:61748"]);
    });
});

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // true em produção
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero // elimina tolerância de tempo
    };
});

var appSetting = new AppSettings();
builder.Configuration.GetSection(nameof(AppSettings)).Bind(appSetting);
builder.Services.AddSingleton(appSetting);

builder.Services.RegisterAllClasses(builder.Configuration);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



var app = builder.Build();

app.UseSwagger(options => options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
app.UseSwaggerUI();




//app.UseHttpsRedirection();
app.UseCors(baseCorsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
