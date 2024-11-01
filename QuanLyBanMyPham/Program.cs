using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<QuanLyBanMyPhamContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuanLyBanMyPhamContext")));
builder.Services.AddControllersWithViews();

// Thêm cấu hình session trước khi gọi builder.Build()
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(24 * 30); // Thời gian tồn tại lâu
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Đặt UseSession sau UseRouting và trước UseAuthorization
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
