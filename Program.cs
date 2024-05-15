using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Data; // Certifique-se de que este namespace corresponde ao que você usou no ApplicationDbContext.cs
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // Adicione isso para MySqlServerVersion
using SimpleApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Adicione serviços ao contêiner.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ProdutoService>();
// Configuração do contexto do banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 25)))); // Altere para a versão do seu servidor MySQL

var app = builder.Build();

// Configure o pipeline de solicitações HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();



// mapeamento de rotas

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.MapControllerRoute(
//     name: "produto",
//     pattern: "produtos/{action=Index}/{id?}",
//     defaults: new { controller = "Produto" });

// app.MapControllerRoute(
//     name: "produtoDetalhes",
//     pattern: "produto/{id}",
//     defaults: new { controller = "Produto", action = "Detalhes" });
    
// app.MapControllerRoute(
//     name: "produtoTodos",
//     pattern: "produto/todos/",
//     defaults: new { controller = "Produto", action = "Todos" });


app.Run();
