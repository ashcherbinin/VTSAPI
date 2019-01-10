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

                var context = serviceScope.ServiceProvider.GetService<ToDoContext>();


            
            var users = new User[]
           {
            new User{FirstMidName="Carson",LastName="Alexander" },
            new User{FirstMidName="Meredith",LastName="Alonso"},
            new User{FirstMidName="Arturo",LastName="Anand"},
            new User{FirstMidName="Gytis",LastName="Barzdukas"},
            new User{FirstMidName="Yan",LastName="Li"},
            new User{FirstMidName="Peggy",LastName="Justice"},
            new User{FirstMidName="Laura",LastName="Norman"},
            new User{FirstMidName="Nino",LastName="Olivetto"}
           };
                foreach (User s in users)
                {
                    context.Users.Add(s);
                }

                var Todoitems = new TodoItem[]
                {
                  new TodoItem { Name = "Walk a dog", isComplete = false },
                  new TodoItem { Name = "Buy Airline tickets", isComplete = false },
                  new TodoItem { Name = "Meeting with Ben", isComplete = false},
                  new TodoItem { Name = "Wholefoods run", isComplete = false},
                  new TodoItem { Name = "Gym", isComplete = false},
                  new TodoItem { Name = "Call Sally", isComplete = false},
                  new TodoItem { Name = "Make a dinner", isComplete = false},
                };
                foreach (TodoItem c in Todoitems)
                {   
                    context.TodoItems.Add(c);
                }

                var todoLists = new TodoList[]
                {
                  new TodoList{UserID = 1,  Name = "Andrey's List", isDefault =false},
                  new TodoList{UserID = 2 , Name = "Andrey's Second List", isDefault =false}
          

           , };

                foreach (TodoList e in todoLists)
                {
                    context.TodoLists.Add(e);
                }

                context.SaveChanges();
               
            }
        
        }

   }
}
