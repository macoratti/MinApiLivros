using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MinApiLivros.Context;
using MinApiLivros.Endpoints;
using MinApiLivros.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ILivroService, LivroService>();

string mySqlConnection =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new
    Exception("A string de conexão 'DefaultConnection' não foi configurada.");

builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization();

var app = builder.Build();

// configura o middleware de exceção
app.UseStatusCodePages(async statusCodeContext
    => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode)
        .ExecuteAsync(statusCodeContext.HttpContext));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/identity/").MapIdentityApi<IdentityUser>();

app.RegisterLivrosEndpoints();

app.Run();