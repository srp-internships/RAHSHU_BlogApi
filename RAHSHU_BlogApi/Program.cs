using Microsoft.EntityFrameworkCore;
using RAHSHU_BlogApi.Data;
using RAHSHU_BlogApi.Repository.PostRepository;
using RAHSHU_BlogApi.Repository.UserRepository;
using RAHSHU_BlogApi.Services;
using RAHSHU_BlogApi.Services.JsonPlaceholderService;
using RAHSHU_BlogApi.Services.PostService;
using RAHSHU_BlogApi.Services.SeedService;
using RAHSHU_BlogApi.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddSingleton<HttpClient>();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJsonPlaceholderService, JsonPlaceholderService>();
builder.Services.AddScoped<ISeedService, SeedService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
