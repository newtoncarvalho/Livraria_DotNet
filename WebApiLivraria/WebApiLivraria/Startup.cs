using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApiLivraria.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Query;
using Microsoft.OData.UriParser;
using Newtonsoft.Json.Serialization;

namespace WebApiLivraria
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /*
         * This method gets called by the runtime. Use this method to add services to the container.       
         * 
         * Aqui sao realizada as injecoes de dependencia no estilo "Convention Over Configuration"
         */
        public void ConfigureServices(IServiceCollection services)
        {
            /**
             * Usando Sintaxe similar ao Lambada expression do Java
             * Vamos especificar o tipo de provedor de banco de dados
             * Neste caso, usaremos apenas o Banco de Dados em Memória
             */
            services.AddDbContext<LivroDbContext>(options =>
                options.UseInMemoryDatabase("LivrariaDB"));

            // Injetando dependencia do framework OData
            services.AddOData();

            // Injetando dependencia do framework AspNet MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // URIs sem caso sensitivo
            services.AddSingleton(sp => new ODataUriResolver {EnableCaseInsensitive = true});
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            /**
             * Configurando OData endpoint
             * MapODataServiceRoute => Declarando existencia de um EndPoint
             */
            //app.UseMvc(builder => builder.MapODataServiceRoute("odata", "apilivraria", GetEdmModel()));
            app.UseMvc(ConfigureODataRoutes);
        }

        private static void ConfigureODataRoutes(Microsoft.AspNetCore.Routing.IRouteBuilder routes)
        {
            var model = GetEdmModel();
            routes.MapODataServiceRoute("ODataRoute", "webapilivraria", model);
            routes.Filter(QueryOptionSetting.Allowed); // Permite o comando $filter
            routes.OrderBy(); // Permite o comando $orderby
            routes.Count(); // Permite o comando $count
            routes.Select(); // Permite o comando $select
        }

        /*
         * Gerando Entity Database Model que sera utilizado pelo OData e pelo
         * EntityManager
         */
        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Livro>("Livros");
            return builder.GetEdmModel();
        }
    }
}
