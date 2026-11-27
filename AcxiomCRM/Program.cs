using Microsoft.AspNetCore.Authentication;
using AcxiomCRM.Data;
using AcxiomCRM.Models;
using AcxiomCRM.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;
var b=WebApplication.CreateBuilder(args);
b.Services.AddDbContext<CrmDb>(o=>o.UseSqlServer(b.Configuration.GetConnectionString("Default")));
b.Services.AddIdentity<ApplicationUser,IdentityRole>(o=>{o.Password.RequiredLength=8;o.User.RequireUniqueEmail=true;o.Lockout.MaxFailedAccessAttempts=5;o.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(15);}).AddEntityFrameworkStores<CrmDb>().AddDefaultTokenProviders();
b.Services.Configure<SecurityStampValidatorOptions>(o=>o.ValidationInterval=TimeSpan.Zero);
b.Services.ConfigureApplicationCookie(o=>{o.LoginPath="/Account/Login";o.AccessDeniedPath="/Account/Denied";o.Cookie.HttpOnly=true;o.Cookie.SecurePolicy=CookieSecurePolicy.Always;o.Events.OnRedirectToLogin=c=>{if(c.Request.Path.StartsWithSegments("/api")) c.Response.StatusCode=401;else c.Response.Redirect(c.RedirectUri);return Task.CompletedTask;};o.Events.OnRedirectToAccessDenied=c=>{if(c.Request.Path.StartsWithSegments("/api")) c.Response.StatusCode=403;else c.Response.Redirect(c.RedirectUri);return Task.CompletedTask;};});
b.Services.AddAuthorization(o=>o.FallbackPolicy=new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
b.Services.AddControllersWithViews(o=>o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
b.Services.AddAntiforgery(o=>o.HeaderName="X-CSRF-TOKEN");
b.Services.AddHttpContextAccessor();b.Services.AddScoped<CrmService>();b.Services.AddScoped<AuditService>();
b.Services.AddRateLimiter(o=>o.AddPolicy("login",c=>RateLimitPartition.GetFixedWindowLimiter(c.Connection.RemoteIpAddress?.ToString()??"unknown",_=>new FixedWindowRateLimiterOptions{PermitLimit=15,Window=TimeSpan.FromMinutes(1)})));
b.Services.AddEndpointsApiExplorer();b.Services.AddSwaggerGen();
var app=b.Build();
app.UseExceptionHandler("/Account/Error");if(!app.Environment.IsDevelopment())app.UseHsts();
app.UseHttpsRedirection();app.UseStaticFiles();app.UseRouting();app.UseRateLimiter();app.UseAuthentication();
app.Use(async (ctx,next)=>{if(ctx.User.Identity?.IsAuthenticated==true){var u=await ctx.RequestServices.GetRequiredService<UserManager<ApplicationUser>>().GetUserAsync(ctx.User);if(u==null||!u.IsActive){await ctx.SignOutAsync(IdentityConstants.ApplicationScheme);ctx.Response.StatusCode=401;return;}}await next();});
app.UseAuthorization();if(app.Environment.IsDevelopment()){app.UseSwagger();app.UseSwaggerUI();}
app.MapControllers();app.MapControllerRoute("default","{controller=Dashboard}/{action=Index}/{id?}");
if(app.Environment.IsDevelopment()&&b.Configuration.GetValue<bool>("Demo:Enabled")){using var scope=app.Services.CreateScope();await Seed.Run(scope.ServiceProvider,b.Configuration);}
app.Run();
public partial class Program {}
