using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts;
using Mijalski.Imagegram.Server.Modules.Accounts.Jwts;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Mijalski.Imagegram.Server", Version = "v1" });
});

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddAccountAuthentication(builder.Configuration);

builder.RegisterDomain();
builder.RegisterModules();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mijalski.Imagegram.Server v1"));
}
app.UseHttpsRedirection();

app.UseAuthentication();

app.MapEndpoints();

app.Run();
