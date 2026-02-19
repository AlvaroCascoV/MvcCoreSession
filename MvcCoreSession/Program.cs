using MvcCoreSession.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//inyectamos el contextAccessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//inyectamos el helper de contextAccessor
builder.Services.AddSingleton<HelperSessionContextAccessor>();



//MEMORIA DISTRIBUIDA PARA QUE SESSION FUNCIONE
builder.Services.AddDistributedMemoryCache();
//CREAMOS UN SERVICIO DE SESSION
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

//entre auth y mvc va session
//HABILITAMOS SESSION PARA EL SERVER
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
