using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using WalLanches.Areas.Admin.Services;
using WalLanches.Context;
using WalLanches.Models;
using WalLanches.Repositories.Interfaces;
using WalLanches.Repositories;
using WalLanches.Services;
using FastReport.Data;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(connection));

FastReport.Utils.RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

builder.Services.Configure<ConfigurationImagens>(builder.Configuration
    .GetSection("ConfigurationPastaImagens"));

builder.Services.AddTransient<ILancheRepository, LancheRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
builder.Services.AddScoped<RelatorioVendasService>();
builder.Services.AddScoped<GraficoVendasService>();
builder.Services.AddScoped<RelatorioLanchesService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin",
        politica =>
        {
            politica.RequireRole("Admin");
        });
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

builder.Services.AddControllersWithViews();

builder.Services.AddPaging(options =>
{
    options.ViewName = "Bootstrap4";
    options.PageParameterName = "pageindex";
});

builder.Services.AddMemoryCache();
builder.Services.AddSession();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseFastReport();
app.UseRouting();

CriarPerfisUsuarios(app);

////cria os perfis
//seedUserRoleInitial.SeedRoles();
////cria os usu�rios e atribui ao perfil
//seedUserRoleInitial.SeedUsers();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
       name: "categoriaFiltro",
       pattern: "Lanche/{action}/{categoria?}",
       defaults: new { Controller = "Lanche", action = "List" });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();

void CriarPerfisUsuarios(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        service.SeedUsers();
        service.SeedRoles();
    }
}