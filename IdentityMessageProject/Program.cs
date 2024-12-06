using FluentValidation.AspNetCore;
using IdentityMessageProject.BusinessLayer.Container;
using IdentityMessageProject.DataAccessLayer.Context;
using IdentityMessageProject.EntityLayer.Concrete;
using IdentityMessageProject.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MessageContext>();

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation();

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<MessageContext>().AddErrorDescriber<CustomIdentityValidator>();

builder.Services.ContainerDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
