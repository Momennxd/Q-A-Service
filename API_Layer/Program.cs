using API_Layer;
using API_Layer.Authorization;
using API_Layer.Security;
using Core_Layer.AppDbContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;




#region init builder

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
    options.Filters.Add<PermissionBasedAuthorizationFilters>();
}).AddNewtonsoftJson();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
               builder.Configuration.GetConnectionString("MyConnection")
               ));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion



#region Jwt Config

JwtOptions? jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {

        options.SaveToken = true; // To access token string within the request

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions!.Issuer,

            ValidateAudience = true,
            ValidAudience = jwtOptions!.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SingingKey)),

            RequireExpirationTime = true,
            ValidateLifetime = true, // Ensure the token's lifetime is validated
            ClockSkew = TimeSpan.Zero // Optional: No tolerance on token expiration
        };

    });

clsToken.jwtOptions = jwtOptions;

#endregion



#region Init App







var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseMiddleware<CustomSessionMiddleware>();




app.MapControllers();

app.Run();

#endregion
