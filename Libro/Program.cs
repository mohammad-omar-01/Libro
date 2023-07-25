using Libro.Data.Mappers;
using Libro.Data.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IBookCopy, BookCopyRepisotory>();
builder.Services.AddScoped<ILibrarian, LibrarianRepository>();
builder.Services.AddScoped<IReservation, ReservationRepoisotory>();

builder.Services.AddDbContext<LibroDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System
            .Text
            .Json
            .Serialization
            .ReferenceHandler
            .Preserve;
    });
builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretKey"])
            )
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "MustBeAdmin",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("Role", "Admin");
        }
    );
    options.AddPolicy(
        "MustBePatron",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("Role", "Patron");
        }
    );
    options.AddPolicy(
        "MustBeLibrarian",
        policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireClaim("Role", "Librarian");
        }
    );
});
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
