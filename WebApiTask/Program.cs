using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApiTask.data;
using WebApiTask.Maping;
using WebApiTask.Repositories;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//  ﬂÊœ  database 
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserDbConnectionString")) );
builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<ITaskRepository, SqlTaskRepository>();
builder.Services.AddDbContext<UserAuthDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserAuthDbConnectionString")) );
// ﬂ «»… Â–« «·ﬂÊœAuthentication
builder.Services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>().AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("UserAuth").AddEntityFrameworkStores<UserAuthDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(option => {
    option.Password.RequireDigit = false;
    option.Password.RequireLowercase = false; 
    option.Password. RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
    option.Password.RequiredLength = 8;
    option.Password.RequiredUniqueChars = 1;


});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
{

    ValidateIssuer = true,
    ValidateAudience   =   true,
    ValidateIssuerSigningKey = true,
    ValidateLifetime = true, 
    ValidIssuer= builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
     IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
}


);
// «÷«›…  autoMapper here
builder.Services.AddAutoMapper(typeof(AutoMapperProfieles));

var app = builder.Build();

 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
