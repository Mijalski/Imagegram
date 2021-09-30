using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Infrastructures.Services;
using Mijalski.Imagegram.Server.Modules.Accounts;
using Mijalski.Imagegram.Server.Modules.Accounts.Jwts;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Imagegram API", Version = "v1" });
});

builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddAccountAuthentication(builder.Configuration);

builder.RegisterDomain();
builder.RegisterModules();

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ICurrentAccountService, CurrentAccountService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Imagegram API"));
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Run();
