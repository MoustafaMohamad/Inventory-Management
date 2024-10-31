
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Common;
using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Profiles;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mail;
using System.Net;
using System.Text;
using DotNetEnv;
using Inventory_Management.Common.Middlewares;
using Hangfire;
using Hangfire.SqlServer;
using Inventory_Management.Features.Common.BackGround_jobs;

namespace Inventory_Management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Enviroment
            Env.Load();
            // Add services to the container.
            builder.Services.AddFluentEmail("maim6349@gmail.com")
           .AddRazorRenderer()  // or AddLiquidRenderer() if you want to use Liquid templates
           .AddSmtpSender(new SmtpClient("smtp.gmail.com")
           {
               UseDefaultCredentials = false,
               Credentials = new NetworkCredential("maim6349@gmail.com", "rzam ngki omum hbgw"),
               EnableSsl = true,
               Port = 587
           });




            builder.Services.AddHangfire(configuration =>
           configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseSqlServerStorage("Server=.;Database=Test22;Trusted_Connection=True;Encrypt=False;",
                                             new SqlServerStorageOptions
                                             {
                                                 CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                                                 SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                                                 QueuePollInterval = TimeSpan.Zero,
                                                 UseRecommendedIsolationLevel = true,
                                                 DisableGlobalLocks = true
                                             }));

            // Add Hangfire Server
            builder.Services.AddHangfireServer();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region MediatR

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            #endregion
            #region AutoFac
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
                builder.RegisterModule(new AutoFacModule()));
            #endregion

            #region AutoMapper
            builder.Services.AddAutoMapper(typeof(UserProfile));
            #endregion

           


            #region Authentication 
            builder.Services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = Environment.GetEnvironmentVariable("ISSUER"),
                        ValidAudience = Environment.GetEnvironmentVariable("AUDIENCE"),
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY")))
                    };
                });
            #endregion



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            // Use Hangfire dashboard for monitoring jobs
            app.UseHangfireDashboard("/hangfire");

            // Register the recurring job
            RecurringJob.AddOrUpdate<SampleJob>(job => job.ExecuteJob(), Cron.Minutely);

            app.UseHttpsRedirection();
            app.UseMiddleware<GlobalErrorHandlerMiddleware>();
            app.UseMiddleware<TransactionMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            MapperHelper.Mapper = app.Services.GetService<IMapper>();
            app.MapControllers();

            app.Run();
        }
    }
}
