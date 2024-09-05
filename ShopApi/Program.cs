using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShopContext>();

// AutoMapper'� ekle
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Dependency Injection (DI) i�in gerekli olan servisleri ekle
builder.Services.AddScoped<IDealerService, DealerManager>();
builder.Services.AddScoped<IDealerDal, EfDealerDal>();

builder.Services.AddScoped<ICampaignService, CampaignManager>();
builder.Services.AddScoped<ICampaignDal, EfCampaignDal>();

builder.Services.AddScoped<IContactService, ContactManager>();
builder.Services.AddScoped<IContactDal, EfContactDal>();

builder.Services.AddScoped<IDealerCategoryService, DealerCategoryManager>();
builder.Services.AddScoped<IDealerCategoryDal, EfDealerCategoryDal>();

builder.Services.AddScoped<INewsService, NewsManager>();
builder.Services.AddScoped<INewsDal, EfNewsDal>();

builder.Services.AddScoped<IVideoService, VideoManager>();
builder.Services.AddScoped<IVideoDal, EfVideoDal>();

// Controllers'� ekle
builder.Services.AddControllers();

// Swagger/OpenAPI'y� ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Uygulama olu�turuluyor
var app = builder.Build();

// HTTP request pipeline'�n� yap�land�r
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();


app.MapControllers();

app.Run();
