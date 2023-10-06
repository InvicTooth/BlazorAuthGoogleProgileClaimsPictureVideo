using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorAuthGoogleProgileClaimsPictureVideo.Data;
using BlazorAuthGoogleProgileClaimsPictureVideo;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//if (!builder.Environment.IsDevelopment())
{
  builder.Services.AddHttpsRedirection(options =>
  {
    options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
    options.HttpsPort = 7240;
  });
}


builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddAuthentication("Cookies")
  .AddCookie(opt => {
    opt.Cookie.Name = "TryingoutGoogleOAuth";
    opt.LoginPath = "/auth/google";
  })
  .AddGoogle(opt => {
    opt.ClientId = builder.Configuration["Google:Id"];
    opt.ClientSecret = builder.Configuration["Google:Secret"];
    opt.Scope.Add("profile");
    opt.Events.OnCreatingTicket = context => {
      string picuri = context.User.GetProperty("picture").GetString();
      context.Identity.AddClaim(new System.Security.Claims.Claim("picture", picuri));
      return Task.CompletedTask;
    };
  })
  ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
