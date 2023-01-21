using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System.Web;

namespace telephonedirectory.Gateway
{
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.Configure<IISServerOptions>(options => {
                options.AllowSynchronousIO = true;
            });
            services.AddCors(c => {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            }); 
            services.Configure<IISOptions>(options => options.AutomaticAuthentication = true);
            services.Configure<AuthSettings>(Configuration.GetSection("AuthSettings"));
            services.AddMvcCore(options => {
                options.EnableEndpointRouting = false;

            }).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);

            services.AddHttpContextAccessor();

            services.AddControllers();

            var mvcBuilder = services.AddMvc();

            services.AddOcelot().AddConsul();
            services.AddControllers();
            services.AddSwaggerForOcelot(Configuration);

            services.AddCors(o =>
            {
                o.AddPolicy("AllowOrigin",
                    builder =>
                    {
                        List<string> corsUrls = new List<string>();
                        Configuration.GetSection("AppSettings").Bind("Cors", corsUrls);
                        builder.WithOrigins(corsUrls.ToArray())
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseSwaggerAuthorized();
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }


            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("AllowOrigin");


            var forwardingOptions = new ForwardedHeadersOptions() { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.All };
            app.UseForwardedHeaders(forwardingOptions);
           
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseResponseCaching();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", "API v1");
                c.RoutePrefix = string.Empty;
            });
            app.UseCors(x => x
                 .AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader());
            app.Use(async (context, next) => {
                context.Response.Headers.Add("X-Xss-Protection", "1;mode=block");// 1:Xss active ,Reflected  XSS durumunda sanitize yapmadan direk render işlemini engeller
                context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
                await next();
            });
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseSwaggerForOcelotUI(Configuration, opt => { opt.DownstreamSwaggerEndPointBasePath = "/swagger/docs"; }).UseOcelot().Wait();
        }
    }
}
