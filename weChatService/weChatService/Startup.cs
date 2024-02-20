using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using weChatService.Model;
using weChatService.Repositories;
using weChatService.Repositories.Interface;
using weChatService.SignalRHub;

namespace weChatService
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            // var ConnectionString = Configuration.GetConnectionString("DefaultConnection");
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
            services.AddSignalR(o => { o.EnableDetailedErrors = true; });
            services.AddSingleton<IDictionary<string, UserConnection>>(opt => new Dictionary<string, UserConnection>());

            //ประกาศใช้ HttpClient ไม่ต้อง new ใหม่
            string BaseUrl = "http://localhost:3000";
            services.AddHttpClient(BaseUrl, config =>
            {
                if (!string.IsNullOrWhiteSpace(BaseUrl))
                {
                    config.BaseAddress = new Uri(BaseUrl);
                    config.DefaultRequestHeaders.Clear();
                }
            });
            // Add your repository
            services.AddSingleton<IChatRepository, ChatRepository>();

            Global.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
               endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}
