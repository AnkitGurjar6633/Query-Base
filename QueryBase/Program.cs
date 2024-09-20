using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QueryBase.DataAccess.DatabaseContext;
using QueryBase.DataAccess.Repository;
using QueryBase.DataAccess.Repository.IRepository;
using QueryBase.Models;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();   /* there are two to authorize and this method enforces authorization policy (user must be authenticated) for all the action methods
  and to allow any user to access methods of a controller add [AllowAnonymous] to the controller but other is done use [Authorize] on the controller which is to be authorized*/

    options.AddPolicy("NotAuthenticated", policy =>     
    {
        policy.RequireAssertion(context =>              //access control depends on this if it returns true or false
        {
            return !context.User.Identity.IsAuthenticated;                      //if this returns true the user can access whaterver this policy is attached to, syntax to attach = [Authorize("policyName")].  ex: for this custom policy [Authorize("NotAuthenticated")]
        }); 
    });

});


builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
});

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


app.UseRouting();           //for identifying action method based on route

app.UseAuthentication();  //for reading identity cookie
app.UseAuthorization();   //Validates access permission of the user
  

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Users}/{controller=Home}/{action=Index}/{id?}");        //execute the filter pipeline (action + filters)

app.Run();
