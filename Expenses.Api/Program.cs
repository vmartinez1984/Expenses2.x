using System.Reflection;
using AutoMapper;
using Expenses.BusinessLayer;
using Expenses.Core.Interfaces.BusinessLayer;
using Expenses.Core.Interfaces.Repository;
using Expenses.Core.Mappers;
using Expenses.Repository;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
// Add Settings
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
// Add services to the container.
builder.Services.AddScoped<IPeriodRepository, PeriodRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
builder.Services.AddScoped<IEntryRepository, EntryRepository>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IPeriodBl, PeriodBl>();
builder.Services.AddScoped<IEntryBl, EntryBl>();
builder.Services.AddScoped<ICategoryBl, CategoryBl>();
builder.Services.AddScoped<IExpenseBl, ExpenseBl>();
builder.Services.AddScoped<ISubcategoryBl, SubcategoryBl>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//Mappers
var mapperConfig = new MapperConfiguration(mapperConfig =>
{
    mapperConfig.AddProfile<ExpenseMapper>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.1",
        Title = "Gastos",
        Description = @"Api Rest para tener registros de los gastos
        ",
        Contact = new OpenApiContact
        {
            Name = "Víctor Martínez",
            Url = new Uri("mailto:ahal_tocob@hotmail.com")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
//services cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}
//app cors
app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
