using Carter;
using eShop;
using eShop.Catalog.Application.Contract;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddCarter();
//builder.Services.AddScoped<IProductManager ,ProductManager>();
//builder.Services.AddScoped<IProductRepository, ProductSqlRepository>();



////database connectionstring 
//builder.Services.AddDbContext<CatalogeContext>(setup =>
//{
//    var cnnstr = builder.Configuration.GetConnectionString("Catalog");
//    setup.UseSqlServer(cnnstr);

//});


//refactoring
builder.Services.AddEShopServices(builder.Configuration, builder.Environment);

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.MapCarter(); 

app.UseCors();

app.Run();
