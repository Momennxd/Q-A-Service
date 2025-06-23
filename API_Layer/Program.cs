using API_Layer.Authorization;
using API_Layer.Exceptions;
using API_Layer.Handlers;
using API_Layer.LogsSettings;
using API_Layer.Telegram;
using CloudinaryDotNet;
using Core.Authorization_Services.Concrete;
using Core.Authorization_Services.Interfaces;
using Core.Options;
using Core.Services.Concrete.Collections;
using Core.Services.Concrete.Institutions;
using Core.Services.Concrete.nsCategories;
using Core.Services.Concrete.Pictures;
using Core.Services.Concrete.Questions;
using Core.Services.Concrete.RefreshTokens;
using Core.Services.Concrete.Users;
using Core.Services.Interfaces;
using Core.Services.Interfaces.Questions;
using Core.Services.Interfaces.RefreshTokens;
using Core.Unit_Of_Work;
using Data.DatabaseContext;
using Data.models.Collections;
using Data.models.Institutions;
using Data.models.nsCategories;
using Data.models.People;
using Data.models.Pictures;
using Data.models.Questions;
using Data.models.RefreshTokens;
using Data.Repositories;
using Data.Repository.Entities_Repositories.Categories_Repo;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.Collections_Repo.Collecs_Questions;
using Data.Repository.Entities_Repositories.Collections_Repo.CollectionsReviews;
using Data.Repository.Entities_Repositories.Collections_Repo.CollectionsSubmitions;
using Data.Repository.Entities_Repositories.Collections_Repo.Collects_Questions;
using Data.Repository.Entities_Repositories.Institutions_Repo;
using Data.Repository.Entities_Repositories.People_Repo;
using Data.Repository.Entities_Repositories.Pics.Choices_Pics_Repo;
using Data.Repository.Entities_Repositories.Pictures.Base;
using Data.Repository.Entities_Repositories.Questions_Repo;
using Data.Repository.Entities_Repositories.Questions_Repo.ChosenChoices;
using Data.Repository.Entities_Repositories.Questions_Repo.nsQuestions_Categories;
using Data.Repository.Entities_Repositories.Questions_Repo.Questions_Choices;
using Data.Repository.Entities_Repositories.RefreshTokens_Repo;
using ExternalAuthentication.Concrete;
using ExternalAuthentication.Interfaces;
using ExternalAuthentication.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Services.Concrete;
using Services.Interfaces;
using System.Net;
using System.Text;
using Telegram.Bot;
using TelegramService.Concrete;
using TelegramService.Interfaces;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;



#region init builder

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Configure(context.Configuration.GetSection("Kestrel"));
});

#region Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        return RateLimitPartition.GetSlidingWindowLimiter(ip, _ => new SlidingWindowRateLimiterOptions
        {
            PermitLimit = 20, // Allowed requests
            Window = TimeSpan.FromSeconds(30),
            SegmentsPerWindow = 1,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 0
        });
    });

    options.RejectionStatusCode = 429;
});
#endregion

#region Configuration

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config/appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

#endregion

#region init serilog
var criticalHandler = new CriticalLogHandler();
builder.Services.AddSingleton(criticalHandler);


builder.Configuration.AddJsonFile("SerilogSettings.json");
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Sink(new CriticalLogSink(criticalHandler))
    .CreateLogger();

builder.Host.UseSerilog();
#endregion


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

builder.Services.AddControllers()
               .AddNewtonsoftJson(options =>
               {
                   // Use the default property (Pascal) casing
                   options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
               });


#region Cloudinary settings
//// Load Cloudinary settings from appsettings.json
var cloudinarySettings = builder.Configuration.GetSection("Cloudinary").Get<CloudinarySettings>();

//// Initialize Cloudinary with the settings
var cloudinaryAccount = new Account(cloudinarySettings.CloudName, cloudinarySettings.ApiKey, cloudinarySettings.ApiSecret);
var cloudinary = new Cloudinary(cloudinaryAccount);


//// Register Cloudinary as a singleton
builder.Services.AddSingleton(cloudinary);


////adding the scope of clouinary
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
#endregion

#region Collections injection
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<ICollectionsAuthService, CollectionsAuthService>();

builder.Services.AddScoped<ICollectionRepo, CollectionRepo>();
builder.Services.AddScoped<IUnitOfWork<ICollectionRepo, QCollection>, UnitOfWork<ICollectionRepo, QCollection>>();
#endregion

#region Users injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUnitOfWork<IUserRepo, User>, UnitOfWork<IUserRepo, User>>();

#endregion

#region Pictures injection
builder.Services.AddScoped<IPicsService, PicsService>();
builder.Services.AddScoped<IPicsRepo, PicsRepo>();
builder.Services.AddScoped<IUnitOfWork<IPicsRepo, Pics>, UnitOfWork<IPicsRepo, Pics>>();
#endregion

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ICategoriesRepo, CategoriesRepo>();
builder.Services.AddScoped<IUnitOfWork<ICategoriesRepo, Categories>, UnitOfWork<ICategoriesRepo, Categories>>();





builder.Services.AddScoped<IChoicesPicsService, ChoicesPicsService>();
builder.Services.AddScoped<IChoicesPicsRepo, ChoicesPicsRepo>();
builder.Services.AddScoped<IUnitOfWork<IChoicesPicsRepo, Choices_Pics>, UnitOfWork<IChoicesPicsRepo, Choices_Pics>>();

builder.Services.AddScoped<IQuestionsService, QuestionsService>();
builder.Services.AddScoped<IQuestionRepo, QuestionRepo>();
builder.Services.AddScoped<IUnitOfWork<IQuestionRepo, Question>, UnitOfWork<IQuestionRepo, Question>>();


builder.Services.AddScoped<ICollectionsQuestionRepo, CollectionsQuestionRepo>();
builder.Services.AddScoped<IUnitOfWork<ICollectionsQuestionRepo, Collections_Questions>,
    UnitOfWork<ICollectionsQuestionRepo, Collections_Questions>>();




builder.Services.AddScoped<IQuestionsChoicesService, QuestionsChoicesService>();
builder.Services.AddScoped<IQuestionsChoicesRepo, QuestionsChoicesRepo>();
builder.Services.AddScoped<IUnitOfWork<IQuestionsChoicesRepo, QuestionsChoices>,
    UnitOfWork<IQuestionsChoicesRepo, QuestionsChoices>>();


builder.Services.AddScoped<IChosenChoicesRepo, ChosenChoicesRepo>();
builder.Services.AddScoped<IUnitOfWork<IChosenChoicesRepo, Chosen_Choices>,
    UnitOfWork<IChosenChoicesRepo, Chosen_Choices>>();



builder.Services.AddScoped<IQuestionsCategoriesService, QuestionsCategoriesService>();
builder.Services.AddScoped<IQuestionsCategoriesRepo, QuestionsCategoriesRepo>();
builder.Services.AddScoped<IUnitOfWork<IQuestionsCategoriesRepo, Questions_Categories>,
    UnitOfWork<IQuestionsCategoriesRepo, Questions_Categories>>();



builder.Services.AddScoped<IAnswerExplanationService, AnswerExplanationService>();
builder.Services.AddScoped<IAnswerExplanationRepo, AnswerExplanationRepo>();
builder.Services.AddScoped<IUnitOfWork<IAnswerExplanationRepo, AnswerExplanation>,
    UnitOfWork<IAnswerExplanationRepo, AnswerExplanation>>();


builder.Services.AddScoped<IInstitutionServce, InstitutionService>();
builder.Services.AddScoped<IInstitutionsRepo, InstitutionsRepo>();
builder.Services.AddScoped<IUnitOfWork<IInstitutionsRepo, Institution>,
    UnitOfWork<IInstitutionsRepo, Institution>>();


//

builder.Services.AddScoped<IChosenChoicesService, ChosenChoicesService>();
builder.Services.AddScoped<IChosenChoicesRepo, ChosenChoicesRepo>();
builder.Services.AddScoped<IUnitOfWork<IChosenChoicesRepo, Chosen_Choices>,
    UnitOfWork<IChosenChoicesRepo, Chosen_Choices>>();

//



builder.Services.AddScoped<ICollectionsReviewsService, CollectionsReviewsService>();
builder.Services.AddScoped<ICollectionsReviewsRepo, CollectionsReviewsRepo>();
builder.Services.AddScoped<IUnitOfWork<ICollectionsReviewsRepo, Collections_Reviews>,
    UnitOfWork<ICollectionsReviewsRepo, Collections_Reviews>>();


builder.Services.AddScoped<ICollectionsSubmitionsService, CollectionsSubmitionsService>();
builder.Services.AddScoped<ICollectionsSubmitionsRepo, CollectionsSubmitionsRepo>();
builder.Services.AddScoped<IUnitOfWork<ICollectionsSubmitionsRepo, Collections_Submitions>,
    UnitOfWork<ICollectionsSubmitionsRepo, Collections_Submitions>>();


builder.Services.AddScoped<IPersonRepo, PersonRepo>();
builder.Services.AddScoped<IUnitOfWork<IPersonRepo, Person>, UnitOfWork<IPersonRepo, Person>>();




builder.Services.AddScoped<ITokenService, TokenService>();



builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IRefreshTokenRepo, RefreshTokenRepo>();
builder.Services.AddScoped<IUnitOfWork<IRefreshTokenRepo, RefreshToken>,
    UnitOfWork<IRefreshTokenRepo, RefreshToken>>();



builder.Services.Configure<GoogleAuthSettings>(
    builder.Configuration.GetSection("Authentication:Google"));
builder.Services.AddScoped<IExternalAuthProvider, GoogleAuthService>();
builder.Services.AddScoped<IExternalAuthProviderFactory, ExternalAuthProviderFactory>();



#region Telegram Injection

builder.Services.Configure<TelegramSettings>(builder.Configuration.GetSection("Telegram"));

builder.Services.AddSingleton<ITelegramBotClient>(provider =>
{
    var settings = provider.GetRequiredService<IOptions<TelegramSettings>>().Value;
    return new TelegramBotClient(settings.Token);
});
builder.Services.AddSingleton<ITelegramBot, clsTBot>();



#endregion

#endregion

#region Security 

#region Hash passwords
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
#endregion

#region Jwt Config

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });


#endregion


#endregion

#region Init App



var app = builder.Build();

var bot = app.Services.GetRequiredService<ITelegramBot>();
var settings = app.Services.GetRequiredService<IOptions<TelegramSettings>>();

criticalHandler.OnCriticalLog += async (msg) =>
{
    await bot.SendMessageAsync(settings.Value.AdminGroupID, $"⚠️ CRITICAL ERROR ⚠️\n @AhmedM204, @MOMEN_xNasr\n\n Log message: \n{msg}");
};


if(app.Environment.IsDevelopment())
{
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
}

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

            if (ex.GetBaseException().GetType() == typeof(SqlException))
            {

                int ErrorCode = ((SqlException)ex.InnerException).Number;

                switch (ErrorCode)
                {
                    case 2627:  // Unique constraint error
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case 547:   // Constraint check violation
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case 2601:  // Duplicated key row error
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // Check for specific exceptions
                switch (ex)
                {
                    case UnauthorizedAccessException:
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case KeyNotFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case NotImplementedException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                        break;
                }
            }



            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorModel
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.InnerException == null ? ex.Message : ex.InnerException.Message
            };

            await context.Response.WriteAsync(errorResponse.ToString());
        }
    });
});

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<CustomSessionMiddleware>();



app.MapControllers();

app.Run();

#endregion
