using ControleDeContatos.Data;
using ControleDeContatos.Helper;
using ControleDeContatos.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleDeContatos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddEntityFrameworkOracle()
               .AddDbContext<BancoContext>(options => options.UseOracle(Configuration.GetConnectionString("Database")));

            //quando chamar a interface, irá implementar a classe http... que é usada em Sessao como injeção de dependencia
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            /**
             * injecao de dependencia da IContatoRepositorio => resolver a classe de implementação        
             * Toda vez que for necessario chamar um metodo do ContatoRepositorio, entao sera utilizada esta
             * linha como injecao de dependencia, tendo IContatoRepositorio a interface invocada, automaticamente
             * irá chamar a implementação (ou seja ContatoRepositorio)
             * */
            services.AddScoped<IContatoRepositorio, ContatoRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<ISessao, Sessao>();
            services.AddScoped<IEmail, Email>();

            services.AddSession(o => {  //guarda informações personalizada do usuario porem só identificando, nao autenticando
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //a ordem importa
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession(); //habilitar sessao, middleware

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
        }
    }
}
