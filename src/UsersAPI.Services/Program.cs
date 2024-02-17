using UsersAPI.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);
builder.Services.AddSwaggerDoc();
builder.Services.AddJwtBearer();

var app = builder.Build();

app.UseSwaggerDoc();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();