using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ReceptenBlog.Data;
using ReceptenBlog.Models;
using ReceptenBlog.Data;
using System.Reflection;
using Microsoft.AspNetCore.Identity.UI.Services;
using RB_Web.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNet.Identity;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ReceptenblogUser>(options => options.SignIn.RequireConfirmedAccount = false) //tijdelijk
     .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddControllers();
//builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)   // use languate identification as suffix
    .AddDataAnnotationsLocalization();          // provide automatic translation of [Display] dataannotation


builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReceptenBlog", Version = "v1" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));

}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.

   // app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    ApplicationDbContext context = new ApplicationDbContext(services.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
    SeedDataContext.Initialize(context);
}
var supportedCultures = new[] { "en-US", "fr", "nl" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
       .AddSupportedCultures(supportedCultures)
       .AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});
app.Run();
