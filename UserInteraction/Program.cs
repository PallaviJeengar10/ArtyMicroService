using Arty.Helper;
using Arty.Models;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedModels.Models;
using System.Security.Claims;
using System.Text;
using UserInteraction.DataRepositories.Carts;
using UserInteraction.DataRepositories.Orders;
using UserInteraction.DataRepositories.WishLists;
using UserInteraction.Services.Carts;
using UserInteraction.Services.Helper;
using UserInteraction.Services.Orders;
using UserInteraction.Services.WishLists;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddDbContext<ArtyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IWishListService, WishListService>();
builder.Services.AddScoped<IWishListRepository, WishListRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MessageConsumer>();
    x.UsingRabbitMq((context, busFactoryConfigurator) =>
    {
        busFactoryConfigurator.ConfigureEndpoints(context);
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = builder.Configuration["Jwt:Issuer"],
               ValidAudience = builder.Configuration["Jwt:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
           };
       });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
    options.AddPolicy("User", policy => policy.RequireClaim(ClaimTypes.Role, "User"));
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
