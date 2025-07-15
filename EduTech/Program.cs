
using EduTech.Extension;
using EduTech.ID_Generator;
using EduTech.IRepositry;
using EduTech.Models;
using EduTech.Models.Context;
using EduTech.Repositories;
using EduTech.Repositry;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EduTech
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ContextEduTech>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IIdentityRepositry, IdentityRepositry>();
            builder.Services.AddScoped<IStudentRepositry, StudentRepositry>();
            builder.Services.AddScoped<IQuizRepository, QuizRepository>();
            builder.Services.AddScoped<IUserQuizRepository, UserQuizRepository>();
            builder.Services.AddScoped<IdGenerator>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
            }).AddEntityFrameworkStores<ContextEduTech>().AddDefaultTokenProviders();
            builder.Services.AddCustomJwtAuth(builder.Configuration);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("https://edu-tech-platform-xi.vercel.app/")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            var app = builder.Build();

            app.UseCors("AllowFrontend");
            app.UseMiddleware<TokenBlacklistMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
