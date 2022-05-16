using Ecommerce01.Data;
using Ecommerce01.Data.Entities;
using Ecommerce01.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Inyeccion de dependencia aca cada ves que llame al DataContext de voy a pasar mi Cadena de Conexion
builder.Services.AddDbContext<DataContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});//en necesario agregar este servicio de forma manual


//TODO : Modificar condiciones del password
//con este servicio agregado de identity de mi tipo user y con los roles de identity
builder.Services.AddIdentity<User, IdentityRole>(cfg =>
{
    //condiciones que defino para los usuarios
    cfg.User.RequireUniqueEmail = true;//tendra 1 email unico
    //condiciones para el password(esta para que sea leve seguridad)
    cfg.Password.RequireDigit = false;////requiere digitos
    cfg.Password.RequiredUniqueChars = 0;//requiere caracteres unicos
    cfg.Password.RequireLowercase = false;//requiere una minuscula
    cfg.Password.RequireNonAlphanumeric = false;//caracter alfanumerico
    cfg.Password.RequireUppercase = false;//requiere una maysucula
    //cfg.Password.RequiredLength = 4;//requiere al menos 4 caracteres
}).AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureApplicationCookie(options =>//configura las cookies
{
    options.LoginPath = "/Account/NoAuthorized";//cuando hay un problema lo llamo al controlador account y al method notauthorized
    options.AccessDeniedPath = "/Account/NoAuthorized";//idem arriba
});



//cada ves que llame al iuser le digo asigname el userhelper, esto es mas que nada para las pruevas unitarias asi despues 
//si quiero puedo cambiar el userhelper por otra clase con otras caracteristicas
builder.Services.AddScoped<IUserHelper,UserHelper>();
builder.Services.AddScoped<ICombosHelper, CombosHelper>();
builder.Services.AddScoped<IBlobHelper, BlobHelper>();
//esta primera es la que uso
builder.Services.AddTransient<SeedDb>();//inyeccion que iny para usar una sola ves
//builder.Services.AddScoped<SeedDb>(); //Esta es la mas usada y inyecta cada ves que se necesita y luego de la borra
//builder.Services.AddSingleton<SeedDb>();// esta inyeccion inyecta una ves pero no lo destruye, lo deja en memoria

var app = builder.Build();

SeedData(); //metodo para incializar mi semilla

void SeedData()
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopedFactory.CreateScope())
    {
        SeedDb? service = scope.ServiceProvider.GetService<SeedDb>();
        service.SeedAsync().Wait();
    }

}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStatusCodePagesWithReExecute("/error/{0}");//cada ves que hay un erro ejecuta y me manda el error y se ejecuta siempre en el homecontroller
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
