using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//NOTE
//Solution'da sağ tıkla manage user screet'i aç ve içine connection string'i yaz gizli şeyler buraya yazılmalı.
//uygulama calısıtıgında apssettings.json ile bu otomatik olarak birşelir.Güvenlik açısından böyle olmalı.
//Bu json C:\Users\barisn\AppData\Roaming\Microsoft\UserSecrets  içerisinde kullanıcı klasöründe unice bir key seklinde json formatında tutulur
//Hassas olacak tüm datalar burada tutulabilir.
//Bu işlemler sadece development ortamında yapılır yani proje developmnet olrak calısıtıgında yapılır ve screetjson dan veriyi okuyup appjson'a yazar
//,production'da bu olmaz (launchSettings'de  "ASPNETCORE_ENVIRONMENT": "Development" DİKKATT!)
//Productionda appsettingjson ile scretjson içindeki veriler birşelmez
//BU 1. YOL
string dbConnectionString = builder.Configuration["ConnectionStrings:SqlConnectionString"];

//NOTE
//BU 2. YOL sadece password 'ü sctreet.jsondan almak için
var builderDBConnection = new SqlConnectionStringBuilder(dbConnectionString);
builderDBConnection.Password = builder.Configuration["ConnectionStrings2Example:Password:SqlPassword"];
string conString = builderDBConnection.ConnectionString;

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
