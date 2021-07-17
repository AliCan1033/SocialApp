using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServerApp.Data;

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
            services.AddControllers().AddNewtonsoftJson();//price bilgisi angular tarafına number burada decimal olduğu için tanıyamadı birde NewtonsoftJson paketini ekleyip burayada bunu ekledik
            services.AddCors(options=>{
                options.AddPolicy(//burada AddPolicy değilde AddDefaultPolicy name bilgisine ve aşşağıda ki app.UseCors(); name bilgisini göndermenize gerek yok eğer çok fazla policy ekliyorsanız name bilgisini kullanmanız gerekir
                    name:MyAllowOrigins,
                    builder =>{
                        builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()//header requestin herhangi bir paremetreye sahip olup olmadığına bakmadan yollar
                        .AllowAnyMethod();//herhang bir metoda göre (post,get,pop,push) karşılayabliriz
                        // .WithMethods("GET");//sadece get istersen POST DELETE PUSH vs ekleyebilirsin 
                        // .AllowAnyOrigin() //herhangi bir adresten gelen talepleri karşılar
                    });    
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowOrigins); //erişim için yukarda hangi corsu verdiysek o etkin olmuş oluyor. Çünkü AddPolicy kısımlarını arttırabiliriz

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
