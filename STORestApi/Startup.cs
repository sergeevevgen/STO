using STOBusinessLogic.BusinessLogics;
using STOContracts.BusinessLogicsContracts;
using STOContracts.StorageContracts;
using STODatabaseImplement.Implements;
//using STOBusinessLogic.MailWorker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using STOContracts.BindingModels;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

namespace STORestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services
        //to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeeStorage, EmployeeStorage>();
            services.AddTransient<ITOStorage, TOStorage>();
            services.AddTransient<ICarStorage, CarStorage>();
            services.AddTransient<IServiceRecordStorage, ServiceRecordStorage>();
            services.AddTransient<IWorkStorage, WorkStorage>();
            services.AddTransient<IWorkTypeStorage, WorkTypeStorage>();
            services.AddTransient<ISparePartStorage, SparePartStorage>();
            services.AddTransient<ITimeOfWorkStorage, TimeOfWorkStorage>();
            services.AddTransient<IStoreKeeperStorage, StoreKeeperStorage>();
            //services.AddTransient<IMessageInfoStorage, MessageInfoStorage>();

            services.AddTransient<IEmployeeLogic, EmployeeLogic>();
            services.AddTransient<ITOLogic, TOLogic>();
            services.AddTransient<ICarLogic, CarLogic>();
            services.AddTransient<IServiceRecordLogic, ServiceRecordLogic>();
            services.AddTransient<IWorkLogic, WorkLogic>();
            services.AddTransient<ISparePartLogic, SparePartLogic>();
            services.AddTransient<ITimeOfWorkLogic, TimeOfWorkLogic>();
            services.AddTransient<IStoreKeeperLogic, StoreKeeperLogic>();
            //services.AddTransient<IMessageInfoLogic, MessageInfoLogic>();
            //services.AddSingleton<AbstractMailWorker, MailKitWorker>();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "STORestApi",
                    Version = "v1"
                });
            });
        }
        // This method gets called by the runtime. Use this method to configure the
        //HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "STORestApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //var mailSender =
            //app.ApplicationServices.GetService<AbstractMailWorker>();
            //mailSender.MailConfig(new MailConfigBindingModel
            //{
            //    MailLogin =
            //Configuration?.GetSection("MailLogin")?.Value.ToString(),
            //    MailPassword =
            //Configuration?.GetSection("MailPassword")?.Value.ToString(),
            //    SmtpClientHost =
            //Configuration?.GetSection("SmtpClientHost")?.Value.ToString(),
            //    SmtpClientPort =
            //Convert.ToInt32(Configuration?.GetSection("SmtpClientPort")?.Value.ToString()),
            //    PopHost = Configuration?.GetSection("PopHost")?.Value.ToString(),
            //    PopPort =
            //Convert.ToInt32(Configuration?.GetSection("PopPort")?.Value.ToString())
            //});
        }
    }
}
