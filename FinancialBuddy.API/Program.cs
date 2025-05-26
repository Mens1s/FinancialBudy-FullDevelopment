using FinancialBuddy.Application.Interfaces.Services;
using FinancialBuddy.Application.Mappings;
using FinancialBuddy.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hangfire;
using Hangfire.SqlServer;
using FinancialBuddy.Infrastructure.BackgroundJobs;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// custom di
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<IUserAssetService, UserAssetService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// JWT
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

// CORS herkese açýk
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


// Antiforgery kaldýr (zaten API ise gerek yok)
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new IgnoreAntiforgeryTokenAttribute());
});

// Hangfire
builder.Services.AddHangfire(config =>
    config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
          {
              CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
              SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
              QueuePollInterval = TimeSpan.Zero,
              UseRecommendedIsolationLevel = true,
              UsePageLocksOnDequeue = true,
              DisableGlobalLocks = true
          }));
builder.Services.AddHangfireServer();

// Swagger ve custom
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();

    var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
    context.Request.Body.Position = 0;

    Console.WriteLine($"Request {context.Request.Method} {context.Request.Path} Body: {body}");

    await next();
});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();

}

app.UseHangfireDashboard("/hangfire");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowAll");

// Recurring Jobs
RecurringJob.AddOrUpdate<MockBankJob>(
    "MockBankDebtSyncJob",
    job => job.SyncCreditCardDebts(),
    Cron.Hourly);

RecurringJob.AddOrUpdate<TransferJob>(
    "ScheduledTransferJob",
    job => job.ProcessScheduledTransfers(),
    Cron.Daily);

RecurringJob.AddOrUpdate<SubscriptionAutoPaymentJob>(
    "SubscriptionAutoPaymentJob",
    job => job.ProcessAutoPayments(),
    Cron.Daily);

RecurringJob.AddOrUpdate<AssetPriceJob>(
    "AssetPriceUpdateJob",
    job => job.UpdateAssetPrices(),
    "*/10 * * * *");

app.Run();
