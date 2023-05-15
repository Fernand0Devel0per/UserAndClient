using CoreLiveCode.BLL;
using CoreLiveCode.BLL.Interfaces;
using CoreLiveCode.DAL;
using CoreLiveCode.DAL.Interfaces;
using CoreLiveCode.Helpers.AutoMapper;
using CoreLiveCode.Helpers.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<IClientDao, ClientDao>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LiveCode V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
