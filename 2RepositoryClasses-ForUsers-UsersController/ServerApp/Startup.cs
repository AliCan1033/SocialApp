using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ServerApp.Data;
using ServerApp.Models;

namespace ServerApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        private readonly string MyAllowOrigins = "_myAllowOrigins";
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SocialContext>(x => x.UseSqlite("Data Source=social.db"));
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<SocialContext>();
            services.AddScoped<ISocialRepository,SocialRepository>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true; //sayı olacak
                options.Password.RequireLowercase = true;//küçük harf olacak
                options.Password.RequireUppercase = true;//büyük harf olacak
                options.Password.RequireNonAlphanumeric = true;//-. _ @ + tarzı olacak 

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);//5 dk ban yersin
                options.Lockout.MaxFailedAccessAttempts = 5;//5 kere yanlış giriş yaparsan 
                options.Lockout.AllowedForNewUsers = true;//username bilgisinin aynısı ile başka username oluşturulamaz

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-. _ @ +";//kullanıcı bunlardan oluşacak 
                options.User.RequireUniqueEmail = true;//1 mail sadece bir kere register olur
            });
            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });//price bilgisi angular tarafına number burada decimal olduğu için tanıyamadı birde NewtonsoftJson paketini ekleyip burayada bunu ekledik
            services.AddCors(options =>
            {
                options.AddPolicy(//burada AddPolicy değilde AddDefaultPolicy name bilgisine ve aşşağıda ki app.UseCors(); name bilgisini göndermenize gerek yok eğer çok fazla policy ekliyorsanız name bilgisini kullanmanız gerekir
                    name: MyAllowOrigins,
                    builder =>
                    {
                        builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()//header requestin herhangi bir paremetreye sahip olup olmadığına bakmadan yollar
                        .AllowAnyMethod();//herhang bir metoda göre (post,get,pop,push) karşılayabliriz
                        // .WithMethods("GET");//sadece get istersen POST DELETE PUSH vs ekleyebilirsin 
                        // .AllowAnyOrigin() //herhangi bir adresten gelen talepleri karşılar
                    });
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {//token için validation
                x.RequireHttpsMetadata = false;//true olursa sadee httpden gelen isteklere bakar
                x.SaveToken = true;//servera kayıt edilsin mi  ?
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,//tokeni yazanın issuer key bilgisi
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Secret").Value)),//token bilggisinin neredetanımladı nasıl geleceği
                    ValidateIssuer = false,//gönderen bilgisi
                    ValidateAudience = false//hangi firmalar için oluşturmuşuz
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<User> userManager)
        {
            if (env.IsDevelopment()) //eğer Proparties>launchSettings.json>"ASPNETCORE_ENVIRONMENT": "Development" demek Production yaparsak yayınlama aşamasına geçmişiz demektir.
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed(userManager).Wait();
            }
            else
            {
                app.UseExceptionHandler(appError=> {
                    
                    appError.Run(async context =>{
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var exception =context.Features.Get<IExceptionHandlerFeature>();
                        if (exception!=null)
                        {
                            //loglama => nlog,elmah kütüphanelerinden alınabilir    
                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = exception.Error.Message
                            }.ToString());
                        }
                    });

                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowOrigins); //erişim için yukarda hangi corsu verdiysek o etkin olmuş oluyor. Çünkü AddPolicy kısımlarını arttırabiliriz

            app.UseAuthentication();//yukardaki user ayarlamaları için 

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
