using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VTSAPI.Repository;
using VTSAPI.Models;
using static VTSAPI.Models.ToDoAPIModel;
using static VTSAPI.Models.UserModel;

namespace VTSAPI
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
            services.AddDbContext<ToDoContext>(opt => opt.UseInMemoryDatabase("ToDoDB"));

            //add dependency injection
            services.AddScoped<ITodoRepository, ToDoRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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

            app.UseHttpsRedirection();

            //Default list
            SeedDatabase(app);

            app.UseMvc();
        }

        private static void SeedDatabase(IApplicationBuilder app)
        {

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {

                var users = new User { Name = "Andrey" };
                var toDoLists = new TodoList { Name = "Andrey's List", isDefault = true, User = users};

                var context = serviceScope.ServiceProvider.GetService<Models.ToDoAPIModel.ToDoContext>();

                context.Users.Add(users);
                context.SaveChangesAsync();

                context.TodoLists.Add (toDoLists);
                context.SaveChangesAsync();

               
               
            }
        
        }

   }
}
