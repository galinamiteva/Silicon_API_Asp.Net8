using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Silicon_ASP_NET_API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.RegisterDbContexts(builder.Configuration);
builder.Services.RegisterJwt(builder.Configuration);
builder.Services.RegisterSwagger();


var app = builder.Build();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());


app.UseSwagger();
app.UseSwaggerUI(x=>x.SwaggerEndpoint("/swagger/v1/swagger.json", "Silicon Web Api v1"));



app.UseHttpsRedirection();
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
