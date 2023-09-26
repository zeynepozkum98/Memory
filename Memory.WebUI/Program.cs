using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Memory.Business.AutoMapper;
using Memory.Business.DependencyResolvers;
using Memory.DataAccess.Concrete.EntityFramework.Context;
using Memory.Entities.Concrete;
using Memory.WebUI.BasketTransaction;
using Memory.WebUI.MiddleWare;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new BusinessModule()));

// Mapper 
MapperConfiguration mapperConfiguration = new MapperConfiguration(mc => mc.AddProfile(new MapperProfile()));
IMapper mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

//Identity
builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>().AddEntityFrameworkStores<MemoryContext>();

//Context

builder.Services.AddDbContext<MemoryContext>();
builder.Services.AddTransient<IBasketTransaction, BasketTransaction>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); // kimlik do?rulama
app.UseAuthorization(); // Yetki do?rulama

app.UseMiddleware<LoggingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=City}/{action=Index}/{id?}");

app.Run();
