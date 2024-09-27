using API_Layer.Authorization;
using API_Layer.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Core.Unit_Of_Work;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using API_Layer.Exceptions;
using System.ComponentModel.DataAnnotations;
using Core.Services.Concrete.Users;
using Core.Services.Concrete.People;
using Core.Services.Concrete.Collections;
using Core.Authorization_Services.Concrete;
using System.Linq.Expressions;
using Core.Services.Interfaces;
using Core.Authorization_Services.Interfaces;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Core;
using Data.DatabaseContext;
using Data.models.Collections;



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

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<ICollectionsAuthService, CollectionsAuthService>();
builder.Services.AddScoped<ICollectionRepo, CollectionRepo>();

builder.Services.AddScoped<IUnitOfWork<ICollectionRepo, QCollection>, UnitOfWork<ICollectionRepo, QCollection>>();






//builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<PeopleService>();
//builder.Services.AddScoped<CollectionsAuthService>();

//builder.Services.AddScoped<ICollectionService, CollectionService>();
//builder.Services.AddScoped<ICollectionsAuthService, CollectionsAuthService>();

//builder.Services.AddScoped<IPersonService, PeopleService>();
//builder.Services.AddScoped<IUserService, UserService>();



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

// Exception Handling Middleware
app.UseExceptionHandler(config =>
{
    config.Run(async context =>
    {
        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            var ex = error.Error;

            // Default to 500 Internal Server Error
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            // Check for specific exceptions
            switch (ex)
            {
                case ValidationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;           
                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ArgumentNullException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case FormatException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
               

                    // Add more cases as needed
            }

            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorModel
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            await context.Response.WriteAsync(errorResponse.ToString());
        }
    });
});



app.MapControllers();

app.Run();

#endregion
