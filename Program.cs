using LessonApi.Infrastructure.AutoMapper;
using LessonApi.Infrastructure.Context;
using LessonApi.Infrastructure.Repositories;
using LessonApi.Infrastructure.Repositories.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("RestFullAPISpec", new OpenApiInfo
    {
        Title = "Lesson API Example",
        Version = "v1",
        Description = "Restfull API Example",
        Contact = new OpenApiContact()
        {
            Email = "mustafa@gmail.com",
            Name = "Mustafa Meral",
            Url = new Uri("https://github.com/yazbabamyaz"),
        },        
    });

    options.IncludeXmlComments(Path.ChangeExtension(typeof(Program).Assembly.Location, ".xml"));
    //Veya
    //var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlCommentFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
    //options.IncludeXmlComments(xmlCommentFullPath);
    //Veya
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    //veya
    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "MyApi.xml"));
});




builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IToken, Token>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddAutoMapper(typeof(Mapping));//herhangi bir class da yazabilirsin.



//jwt kimlik do�rulama - yetkilendirme
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["AppSettings:ValidIssuer"],
        ValidAudience = builder.Configuration["AppSettings:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Secret"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

//builder.Services.AddAuthorization();//yetkilendirme hizmetlerini yap�land�rmak i�in kullan�l�r.
//Bir endpoint i�in kimlik do�rulama ya da role sahip olmas� i�in policy gerekli olabilir
//Policy using a Role
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
});
//RequireAdminRole ad�nda policy tan�mlad�k ve admin rol�n� sahip bunu controller ya da metoda
//[Authorize] �zniteli�ini kullan�r�z

//bu ise claim bazl� policy �rne�idir:Tokenda userName claim'i ve de�eri de mustafa olacak.

builder.Services.AddAuthorization(options=>
{
    options.AddPolicy("RequireClaim", policy => policy.RequireClaim("userName","mustafa"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/RestFullAPISpec/swagger.json", "RestFullAPI v1"));
    //endpoint belirtiyorum ki kullan�c�lar isterlerse yard�m d�k�man�na eri�sin.
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


//Bu �al��malar sonras�nda XML comment'lerinin bir dosya olarak Bin klas�r� alt�nda olu�tu�unu da g�rebiliriz.