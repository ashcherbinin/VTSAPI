using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static VTSAPI.Models.UserModel;

namespace VTSAPI.Models
{
    public class ToDoAPIModel
    {
       
        public class TodoList
        {   
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; }
            public bool isDefault { get; set; } = false;
            public User User { get; set; }

        }

        public class TodoItem
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsComplete { get; set; }
       
        }



        public class ToDoContext : DbContext
        {
            public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
            {

            }

            public DbSet<User> Users { get; internal set; }
            public DbSet<TodoList> TodoLists { get; internal set; }
            public DbSet<TodoItem> TodoItems { get; internal set; }
           


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>().ToTable("Users");
                modelBuilder.Entity<TodoList>().ToTable("TodoLists");
                modelBuilder.Entity<TodoItem>().ToTable("TodoItems");
            
        
            }


        }

    }
}
